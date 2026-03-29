using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;
    public Vector2 MoveInput {get; private set;}
    public bool ScanInput;
    public bool InteractInput;

    private void Awake() 
    {
         if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
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
    public void onInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractInput = true;
        }
        else if (context.canceled)
        {
            InteractInput = false;
        }
    }
}
