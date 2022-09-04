namespace Game.Input
{
    using UnityEngine;
    using UnityEngine.Events;

    public class PlayerMouseInput : MonoBehaviour
    {
        public UnityEvent<Vector3> PointerClicked;

        private void Update()
        {
            DetectPointerClicked();
        }

        private void DetectPointerClicked()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PointerClicked?.Invoke(Input.mousePosition);
            }
        }
    }
}