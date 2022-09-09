namespace Game.Players
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    public class PlayerMouseInput : MonoBehaviour
    {
        public UnityEvent<Vector3> PointerClicked;

        private void Update()
        {
            DetectPointerClicked();
        }

        private void DetectPointerClicked()
        {
            if (Touchscreen.current.IsPressed())
            {
                PointerClicked?.Invoke(Touchscreen.current.position.ReadValue());
            }        
            if (Mouse.current.leftButton.isPressed)
            {
                PointerClicked?.Invoke(Mouse.current.position.ReadValue());
            }
        }
    }
}