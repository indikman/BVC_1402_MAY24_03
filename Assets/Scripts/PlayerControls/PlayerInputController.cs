using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
	
	#region Serializable Fields
	
	#endregion
	#region Private Variables
	PlayerController playerController;
	#endregion
	// Start is called before the first frame update
	void Awake()
	{
		playerController = GetComponent<PlayerController>();
	}

	private void OnEnable()
	{
		PlayerInputs playerInputs = new PlayerInputs();

		// Jump press and release
		playerInputs.PlayerActions.Jump.performed += OnJumpPressed;
		playerInputs.PlayerActions.Jump.canceled += OnJumpReleased;
		
		// Move
		playerInputs.PlayerMovement.Move.performed += OnMovePerformed;
		
		playerInputs.Enable();
	}

	private void OnMovePerformed(InputAction.CallbackContext context)
	{
		Vector2 movementValues = context.ReadValue<Vector2>();
		playerController.Move(movementValues);
	}

	private void OnJumpPressed(InputAction.CallbackContext context)
	{
		playerController.HandleJump();
	}
	
	private void OnJumpReleased(InputAction.CallbackContext context)
	{
		playerController.JumpReleased();
	}
	
	


	/*void OnEnable()
	{
		PlayerInputs playerInputs = new PlayerInputs();
		if(playerInputs != null)
		{
			playerInputs.PlayerActions.Jump.performed += (val)=>playerController.HandleJump();
			playerInputs.PlayerActions.Jump.canceled += (val)=>playerController.JumpReleased();
			playerInputs.PlayerMovement.Move.performed +=(val)=>playerController.Move(val.ReadValue<Vector2>());
		}
		playerInputs.Enable();
		
	}*/
	
}
