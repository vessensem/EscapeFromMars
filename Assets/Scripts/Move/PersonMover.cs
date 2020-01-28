using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
            private CharacterController _characterController;
            private Vector3 _moveDirection = Vector3.zero;
            private Vector2 _controlVector;
            private bool canMove = true;

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

            void Awake()
            {
                _characterController = GetComponent<CharacterController>();
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

            public void Move(InputAction.CallbackContext context)
            {
                _controlVector = context.action.ReadValue<Vector2>();

                if (_controlVector.x < 0)
                    transform.eulerAngles = new Vector3(0, 180, 0);
                else
                if (_controlVector.x > 0)
                    transform.eulerAngles = new Vector3(0, 0, 0);
            }

            public void Jump(InputAction.CallbackContext context)
            {
                if (_characterController.isGrounded && CanMove)
                {
                    _moveDirection.y = JumpSpeed;
                }
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