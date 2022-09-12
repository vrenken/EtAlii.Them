namespace Game.Players
{
	using UnityEngine;
	using UnityEngine.InputSystem;

	public class PlayerMovement : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public InputActionsSource inputActionsSource;

		private void OnEnable()
		{
			inputActionsSource.InputActions.Player.Move.performed += OnMove;
			inputActionsSource.InputActions.Player.Move.canceled += OnMoveCompleted;
			inputActionsSource.InputActions.Player.Look.performed += OnLook;
			inputActionsSource.InputActions.Player.Look.canceled += OnLookCompleted;
			inputActionsSource.InputActions.Player.Sprint.performed += OnSprint;
			inputActionsSource.InputActions.Player.Jump.performed += OnJump;
		}

		private void OnDisable()
		{
			inputActionsSource.InputActions.Player.Move.performed -= OnMove;
			inputActionsSource.InputActions.Player.Move.canceled -= OnMoveCompleted;
			inputActionsSource.InputActions.Player.Look.performed -= OnLook;
			inputActionsSource.InputActions.Player.Look.canceled -= OnLookCompleted;
			inputActionsSource.InputActions.Player.Sprint.performed -= OnSprint;
			inputActionsSource.InputActions.Player.Jump.performed -= OnJump;
		}

		private void OnMove(InputAction.CallbackContext context) => move = context.ReadValue<Vector2>();
		private void OnMoveCompleted(InputAction.CallbackContext obj) => move = Vector2.zero;

		private void OnLook(InputAction.CallbackContext context)
		{
			if(cursorInputForLook)
			{
				look = context.ReadValue<Vector2>();
			}
		}

		private void OnLookCompleted(InputAction.CallbackContext context) => look = Vector2.zero;

		private void OnJump(InputAction.CallbackContext context)
		{
			var isPressed = context.ReadValueAsButton();
			JumpInput(isPressed);
		}

		private void OnSprint(InputAction.CallbackContext context) 
		{
			// We do not want to change the sprint.
			// sprint = context.ReadValueAsButton();
		}

		private void JumpInput(bool newJumpState) => jump = newJumpState;

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}