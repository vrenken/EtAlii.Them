namespace Game.Players
{
    using System;
    using UnityEngine;

    public class InputActionsSource : MonoBehaviour
    {
        public PlayerInputActions InputActions => _inputActions.Value;
        private readonly Lazy<PlayerInputActions> _inputActions = new (() => new PlayerInputActions());
        
        private void Awake()
        {
            _inputActions.Value.Player.Enable();
        }
    }
}
