using UnityEngine;
using System.Collections;
//-----------------------------------------------------------
//Enum defining all possible game events
//More events should be added to the list
public enum EVENT_TYPE
{
    //Анимации
    START_PLAYER_ANIMATION = 0,
    END_PLAYER_ANIMATION = 1,
    EVENT_PLAYER_ANIMATION = 2,
    START_ENEMY_ANIMATION = 7,
    END_ENEMY_ANIMATION = 8,
    EVENT_ENEMY_ANIMATION = 9,

    GET_DAMAGE = 3,//Получили демаг (Param=ArrayList(GameObject,(float)Damage))
    CHARACTER_DIED = 4,// Param = GameObject
    EMPTY_EVENT = 5    
};
//-----------------------------------------------------------
//Listener interface to be implemented on Listener classes
public interface IListener
{
    //Notification function to be invoked on Listeners when events happen
    void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null);
}
//-----------------------------------------------------------