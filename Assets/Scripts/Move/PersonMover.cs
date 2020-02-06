using UnityEngine;
using Zenject;

namespace EscapeFromMars
{
    namespace Controls
    {
        [RequireComponent(typeof(CharacterController))]
        public class PersonMover : MonoBehaviour, IListener
        {
            [SerializeField]
            private float _acceleration = 5f;
            [SerializeField]
            private float _speed = 6.0f;
            [SerializeField]
            private float _jumpSpeed = 8.0f;
            [SerializeField]
            private float _gravity = 20.0f;

            private EventManager _eventManager;
            private bool canMove = true;

            protected CharacterController _characterController;
            protected Vector3 _moveDirection = Vector3.zero;
            protected Vector2 _controlVector;

            public Vector3 MoveDirectionSpeed { get => new Vector3(_moveDirection.x * _speed, _moveDirection.y, 0); }
            public float NormalizedSpeed { get => Mathf.Abs(MoveDirectionSpeed.x) / _speed; }
            public bool IsGrounded { get => _characterController.isGrounded; }
            public bool CanMove { get { return canMove; } set { canMove = value; if (!value) _moveDirection.x = 0; } }

            [Inject]
            private void Constructor(EventManager eventManager, CharacterController characterController)
            {
                _eventManager = eventManager;
                _characterController = characterController;
            }

            private void Start()
            {
                _eventManager.AddListener(EVENT_TYPE.CHARACTER_DIED, this);
            }

            private void Update()
            {
                if (CanMove)
                    Move();
            }

            private void Move()
            {
                _moveDirection.x = Mathf.Lerp(_moveDirection.x, _controlVector.x, Time.deltaTime * _acceleration);
                _moveDirection.y -= _gravity * Time.deltaTime;
                _characterController.Move(MoveDirectionSpeed * Time.deltaTime);
            }

            private void SetRotation()
            {
                if (_controlVector.x < 0)
                    transform.eulerAngles = new Vector3(0, 180, 0);
                else
                  if (_controlVector.x > 0)
                    transform.eulerAngles = new Vector3(0, 0, 0);
            }

            private void Die(GameObject character)
            {
                if (!character.Equals(gameObject))
                    return;
                canMove = false;
            }

            public void SetMoveDirection(Vector3 controlVector)
            {
                _controlVector = controlVector;
                SetRotation();
            }

            public void Stop()
            {
                _controlVector = Vector3.zero;
            }

            public void Jump()
            {
                if (_characterController.isGrounded && CanMove)
                {
                    _moveDirection.y = _jumpSpeed;
                }
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