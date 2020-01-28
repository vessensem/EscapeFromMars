using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
//-----------------------------------------------------------
//Синглтон EventManager для отправки событий получателем
//Работает с реализациями IListener 
public class EventManager : MonoBehaviour
{
    public EVENT_TYPE[] eventsDebug;

    public bool dbgShowMessages;
    #region C# Свойства
    //-----------------------------------------------------------
    //Общий доступ к экземпляру
    public static EventManager Instance
    {
        get { return instance; }
        set { }
    }
    #endregion

    #region Переменные
    //Экземпляр диспетчера событий(singleton design pattern)
    private static EventManager instance = null;

    //Массив получателей(все зарегистрировавшиеся объекты)
    private Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();
    #endregion
    //-----------------------------------------------------------
    #region methods
    //Called at start-up to initialize
    void Awake()
    {
        //Если экземпляр отсутсвует, сохранить данный экземпляр
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Prevent object from being destroyed on scene exit
        }
        else //Instance already exists, so destroy this one. This should be a singleton object
            DestroyImmediate(this);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    [ContextMenu("Послать eventsDebug одновременно")]
    void DoEventsDebug()
    {
        foreach (var item in eventsDebug)
        {
            PostNotification(item, this);
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    //Called on scene change. Clean up dictionary
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        RemoveRedundancies();
    }

    //-----------------------------------------------------------
    /// <summary>
    /// Функция добавления получателя в массив
    /// </summary>
    /// <param name="Event_Type">Событие, ожидаемое получателем</param>
    /// <param name="Listener">Объект, ожидающий события</param>
    public void AddListener(EVENT_TYPE Event_Type, IListener Listener)
    {
        //List of listeners for this event
        List<IListener> ListenList = null;

        //New item to be added. Check for existing event type key. If one exists, add to list
        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            //List exists, so add new item
            ListenList.Add(Listener);
            return;
        }

        //Otherwise create new list as dictionary key
        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList); //Add to internal listeners list
    }
    //-----------------------------------------------------------
    /// <summary>
    /// Посылает события получателям
    /// </summary>
    /// <param name="Event_Type">Событие для вызова</param>
    /// <param name="Sender">Вызываемый объект</param>
    /// <param name="Param">Optional argument</param>
    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        if (dbgShowMessages)
            Debug.Log("Send message|" + Event_Type + "|\nSender|" + Sender);
        //Notify all listeners of an event

        //List of listeners for this event only
        List<IListener> ListenList = null;

        //If no event entry exists, then exit because there are no listeners to notify
        if (!Listeners.TryGetValue(Event_Type, out ListenList))
            return;

        //Entry exists. Now notify appropriate listeners
        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null)) //If object is not null, then send message via interfaces
                ListenList[i].OnEvent(Event_Type, Sender, Param);
        }
    }
    //-----------------------------------------------------------
    //Remove event type entry from dictionary, including all listeners
    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        //Remove entry from dictionary
        Listeners.Remove(Event_Type);
    }
    //-----------------------------------------------------------
    //Remove all redundant entries from the Dictionary
    public void RemoveRedundancies()
    {
        //Create new dictionary
        Dictionary<EVENT_TYPE, List<IListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();

        //Cycle through all dictionary entries
        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> Item in Listeners)
        {
            //Cycle through all listener objects in list, remove null objects
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                //If null, then remove item
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }

            //If items remain in list for this notification, then add this to tmp dictionary
            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }

        //Replace listeners object with new, optimized dictionary
        Listeners = TmpListeners;
    }
    //-----------------------------------------------------------

    #endregion
}
