using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput {get; private set;}
    public bool ScanInput;
    public bool InteractInput;

    public void onMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            MoveInput = Vector2.zero;
        }
    }

    public void onScanInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ScanInput = true;
        }
    }
}
