using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CharacterController {
    private int moveChoice;

    // Use this for initialization
    void Start()
    {
        hp = 3;
        atk = 1;

        currentPosition = CharacterPosition.Middle;
        currentAction = CharacterAction.None;

        sprite = GetComponent<SpriteRenderer>();

        moveLocked = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (!moveLocked)
        {
            PerformMove();
        }
	}

    private void PerformMove()
    {
        if (currentPosition == CharacterPosition.Left)
        {
            #region Left AI Choice
            moveChoice = Random.Range(0, 4);

            switch (moveChoice)
            {
                case 0:
                    currentAction = CharacterAction.MoveRight;
                    break;
                case 1:
                    currentAction = CharacterAction.AttackStraight;
                    break;
                case 2:
                    currentAction = CharacterAction.AttackRight;
                    break;
                case 3:
                    currentAction = CharacterAction.Block;
                    break;
            }

            moveLocked = true;
            #endregion Left AI Choice
        }
        else if (currentPosition == CharacterPosition.Middle)
        {
            #region Middle AI Choice
            moveChoice = Random.Range(0, 6);

            switch (moveChoice)
            {
                case 0:
                    currentAction = CharacterAction.MoveLeft;
                    break;
                case 1:
                    currentAction = CharacterAction.MoveRight;
                    break;
                case 2:
                    currentAction = CharacterAction.AttackLeft;
                    break;
                case 3:
                    currentAction = CharacterAction.AttackStraight;
                    break;
                case 4:
                    currentAction = CharacterAction.AttackRight;
                    break;
                case 5:
                    currentAction = CharacterAction.Block;
                    break;
            }

            moveLocked = true;
            #endregion Middle AI Choice
        }
        else if (currentPosition == CharacterPosition.Right)
        {
            #region Right AI Choice
            moveChoice = Random.Range(0, 4);

            switch (moveChoice)
            {
                case 0:
                    currentAction = CharacterAction.MoveLeft;
                    break;
                case 1:
                    currentAction = CharacterAction.AttackLeft;
                    break;
                case 2:
                    currentAction = CharacterAction.AttackStraight;
                    break;
                case 3:
                    currentAction = CharacterAction.Block;
                    break;
            }

            moveLocked = true;
            #endregion Right AI Choice
        }
    }
}
