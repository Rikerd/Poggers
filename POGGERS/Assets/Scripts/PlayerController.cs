using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController {

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start ()
    {
        hp = 3;

        currentPosition = CharacterPosition.Middle;
        currentAction = CharacterAction.None;

        sprite = GetComponent<SpriteRenderer>();

        moveLocked = false;
	}

    // Update is called once per frame
    void Update()
    {

        if (currentAction == CharacterAction.Block)
        {
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = Color.white;
        }

        if (!moveLocked)
        {
            #region Left Move Input
            // ==========================================
            // |                                        |
            // |            Left Move Input             |
            // |                                        |
            // ==========================================

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentPosition == CharacterPosition.Middle)
                {
                    // If player is in the middle, move them to the left
                    currentPosition = CharacterPosition.Left;
                    currentAction = CharacterAction.MoveLeft;

                    moveLocked = true;
                }
                else if (currentPosition == CharacterPosition.Right)
                {
                    // If player is on the right, move to the middle
                    currentPosition = CharacterPosition.Middle;
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

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentPosition == CharacterPosition.Middle)
                {
                    // If player is in the middle, move them to the right
                    currentPosition = CharacterPosition.Right;
                    currentAction = CharacterAction.MoveRight;

                    moveLocked = true;
                }
                else if (currentPosition == CharacterPosition.Left)
                {
                    // If player is on the left, move to the middle
                    currentPosition = CharacterPosition.Middle;
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

            if (Input.GetKeyDown(KeyCode.S))
            {
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
            if (Input.GetKeyDown(KeyCode.Q) && currentPosition != CharacterPosition.Left)
            {
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

            if (Input.GetKeyDown(KeyCode.W))
            {
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
            if (Input.GetKeyDown(KeyCode.E) && currentPosition != CharacterPosition.Right)
            {
                // Sets action to attack right
                currentAction = CharacterAction.AttackRight;

                moveLocked = true;
            }
            #endregion Right Attack Input
        }
    }
}
