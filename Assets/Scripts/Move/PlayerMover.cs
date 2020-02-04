using UnityEngine;
using UnityEngine.InputSystem;

namespace EscapeFromMars.Controls
{
    public class PlayerMover : PersonMover
    {
        public void PlayerMove(InputAction.CallbackContext context)
        {
            Move(context.action.ReadValue<Vector2>());
        }

        public void PlayerJump(InputAction.CallbackContext context)
        {
            if (_characterController.isGrounded && CanMove)
            {
                _moveDirection.y = JumpSpeed;
            }
        }
    }
}