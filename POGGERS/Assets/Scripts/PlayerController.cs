using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //[HideInInspector]
    public CharacterPosition currentPosition;

    //[HideInInspector]
    public CharacterAction currentAction;

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        currentPosition = CharacterPosition.Middle;
        currentAction = CharacterAction.None;

        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (currentAction == CharacterAction.Block)
        {
            sprite.color = Color.red;
        } else
        {
            sprite.color = Color.white;
        }

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
                transform.position += new Vector3(-2, 0, 0);
            }
            else if (currentPosition == CharacterPosition.Right)
            {
                // If player is on the right, move to the middle
                currentPosition = CharacterPosition.Middle;
                currentAction = CharacterAction.MoveLeft;
                transform.position += new Vector3(-2, 0, 0);
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
                transform.position += new Vector3(2, 0, 0);
            }
            else if (currentPosition == CharacterPosition.Left)
            {
                // If player is on the left, move to the middle
                currentPosition = CharacterPosition.Middle;
                currentAction = CharacterAction.MoveRight;
                transform.position += new Vector3(2, 0, 0);
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
        }
        #endregion Right Attack Input
    }
}
