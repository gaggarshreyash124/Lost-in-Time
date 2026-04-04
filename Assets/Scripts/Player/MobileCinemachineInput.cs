using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.Cinemachine;

public class MobileCinemachineInput : MonoBehaviour,AxisState.IInputAxisProvider
{
    public float sensitivity = 0.2f;

    Vector2 lookInput;

    void Update()
    {
        lookInput = Vector2.zero;

        if (Touchscreen.current == null)
            return;

        foreach (var touch in Touchscreen.current.touches)
        {
            if (!touch.press.isPressed)
                continue;

            int id = touch.touchId.ReadValue();

            // BLOCK UI TOUCHES
            if (EventSystem.current.IsPointerOverGameObject(id))
                continue;

            lookInput = touch.delta.ReadValue() * sensitivity;
        }
    }

    public float GetAxisValue(int axis)
    {
        switch(axis)
        {
            case 0:
                return lookInput.x;

            case 1:
                return lookInput.y;
        }

        return 0;
    }
}
