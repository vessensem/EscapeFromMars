using UnityEngine;
using Zenject;

namespace EscapeFromMars
{
    namespace Controls
    {
        [RequireComponent(typeof(CharacterController))]
        public class PersonMover : MonoBehaviour, IListener
        {
            public float Acceleration = 5f;
            public float Speed = 6.0f;
            public float JumpSpeed = 8.0f;
            public float Gravity = 20.0f;

            private EventManager _eventManager;
            private bool canMove = true;

            protected CharacterController _characterController;
            protected Vector3 _moveDirection = Vector3.zero;
            protected Vector2 _controlVector;

            public Vector3 MoveDirectionSpeed { get => new Vector3(_moveDirection.x * Speed, _moveDirection.y, 0); }
            public float NormalizedSpeed { get => Mathf.Abs(MoveDirectionSpeed.x) / Speed; }
            public bool IsGrounded { get => _characterController.isGrounded; }
            public bool CanMove { get { return canMove; } set { canMove = value; if (!value) _moveDirection.x = 0; } }

            [Inject]
            void Constructor(EventManager eventManager, CharacterController characterController)
            {
                _eventManager = eventManager;
                _characterController = characterController;
            }

            void Start()
            {
                _eventManager.AddListener(EVENT_TYPE.CHARACTER_DIED, this);
            }

            void Update()
            {
                if (CanMove)
                {
                    CheckSpeed();
                    _moveDirection.y -= Gravity * Time.deltaTime;
                    _characterController.Move(MoveDirectionSpeed * Time.deltaTime);
                }
            }

            void CheckSpeed()
            {
                _moveDirection.x = Mathf.Lerp(_moveDirection.x, _controlVector.x, Time.deltaTime * Acceleration);
            }

            protected void Move(Vector3 controlVector)
            {
                _controlVector = controlVector;
                SetRotation();
            }

            void SetRotation()
            {
                if (_controlVector.x < 0)
                    transform.eulerAngles = new Vector3(0, 180, 0);
                else
                  if (_controlVector.x > 0)
                    transform.eulerAngles = new Vector3(0, 0, 0);
            }

            void Die(GameObject character)
            {
                if (!character.Equals(gameObject))
                    return;
                canMove = false;
            }

            public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
            {
                switch (Event_Type)
                {
                    case EVENT_TYPE.CHARACTER_DIED:
                        Die((GameObject)Param);
                        break;
                }
            }
        }
    }
}