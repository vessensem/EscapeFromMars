using UnityEngine;
using UnityEngine.InputSystem;

namespace EscapeFromMars.Controls
{
    public class PlayerMover : PersonMover
    {
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
    }
}