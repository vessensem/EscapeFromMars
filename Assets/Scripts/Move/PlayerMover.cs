using UnityEngine;
using UnityEngine.InputSystem;

namespace EscapeFromMars.Controls
{
    public class PlayerMover : PersonMover
    {
        public void PlayerMove(InputAction.CallbackContext context)
        {
            Run(context.action.ReadValue<Vector2>());
        }

        public void PlayerJump(InputAction.CallbackContext context)
        {
            Jump();
        }
    }
}