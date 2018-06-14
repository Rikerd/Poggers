using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : CharacterController {

	private float initialX;
	private float currentX;
	private float Xdirection;

	private float initialY;
	private float currentY;
	private float Ydirection;

	// Use this for initialization
	void Start ()
	{
		hp = 3;
		atk = 1;

		currentPosition = CharacterPosition.Middle;
		currentAction = CharacterAction.None;

		sprite = GetComponent<SpriteRenderer>();

		moveLocked = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (!moveLocked)
		{
			

			#region Left Move Input
			// ==========================================
			// |                                        |
			// |            Left Move Input             |
			// |                                        |
			// ==========================================

			if (ReturnDirection () == "Move-Left")
			{
				ResetDirection();
				if (currentPosition != CharacterPosition.Left)
				{
					currentAction = CharacterAction.MoveLeft;

					moveLocked = true;

				}
			}
			#endregion Left Move Input

			#region Right Move Input
			// ==========================================
			// |                                        |
			// |            Right Move Input            |
			// |                                        |
			// ==========================================

			if (ReturnDirection () == "Move-Right")
			{
				ResetDirection();
				if (currentPosition != CharacterPosition.Right)
				{
					currentAction = CharacterAction.MoveRight;

					moveLocked = true;

				}
			}
			#endregion Right Move Input

			#region Block Input
			// ==========================================
			// |                                        |
			// |               Block Input              |
			// |                                        |
			// ==========================================

			if (ReturnDirection () == "Block")
			{
				ResetDirection();
				// Sets action to block
				currentAction = CharacterAction.Block;

				moveLocked = true;

			}
			#endregion Block Input

			#region Left Attack Input
			// ==========================================
			// |                                        |
			// |           Left Attack Input            |
			// |                                        |
			// ==========================================

			// Checks if that position is not on the left as well
			if (ReturnDirection() == "Attack-Left" && currentPosition != CharacterPosition.Left)
			{
				ResetDirection();
				// Sets action to attack left
				currentAction = CharacterAction.AttackLeft;

				moveLocked = true;

			}
			#endregion Left Attack Input

			#region Straight Attack Input
			// ==========================================
			// |                                        |
			// |         Straight Attack Input          |
			// |                                        |
			// ==========================================

			if (ReturnDirection () == "Attack-Center")
			{
				ResetDirection();
				// Sets action to attack left
				currentAction = CharacterAction.AttackStraight;

				moveLocked = true;

			}
			#endregion Straight Attack Input

			#region Right Attack Input
			// ==========================================
			// |                                        |
			// |          Right Attack Input            |
			// |                                        |
			// ==========================================

			// Checks if that position is not on the right as well
			if (ReturnDirection () == "Attack-Right" && currentPosition != CharacterPosition.Right)
			{
				ResetDirection();
				// Sets action to attack right
				currentAction = CharacterAction.AttackRight;

				moveLocked = true;

			}
			#endregion Right Attack Input
		}
	}


	// V V MOUSE DRAG INPUT V V
	void OnMouseDown() {
		initialX = Input.mousePosition.x;
		initialY = Input.mousePosition.y;
	}

	void OnMouseUp() {
		currentX = Input.mousePosition.x;
		Xdirection = initialX - currentX;

		currentY = Input.mousePosition.y;
		Ydirection = initialY - currentY;

		Debug.Log(ReturnDirection ());
	}

	string ReturnDirection() {
		string result;

		if (Ydirection < -100) {
			if (Xdirection > 120){
				result = "Attack-Left";
				return result;
			} else if (Xdirection < -120) {
				result = "Attack-Right";
				return result;
			} else {
				result = "Attack-Center";
				return result;
			}
		} else if (Ydirection > 180) {
			result = "Block";
			return result;
		} else if (Mathf.Abs (Xdirection) > 100) {
			if (Xdirection > 100) {
				result = "Move-Left";
				return result;
			} else if (Xdirection < -100) {
				result = "Move-Right";
				return result;
			}
		}

		result = "error";
		return result;
	}

	void ResetDirection () {
		initialX = 0;
		initialY = 0;
		Xdirection = 0;

		currentX = 0;
		currentY = 0;
		Ydirection = 0;

		Debug.Log ("reset");
	}
}
