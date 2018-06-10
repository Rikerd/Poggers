using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour {
    protected int hp;

    // Prevents futher action if turn is complete until reset by the BattleManager
    protected bool moveLocked;

    // Where the player is (left, middle, right)
    protected CharacterPosition currentPosition;

    // What the player is doing for the turn
    protected CharacterAction currentAction;

    #region Battle Damage Checks/Calculations
    // Opposite checks because position is realtive to model POV
    public void performAttack(CharacterController enemyController)
    {
        if (currentAction == CharacterAction.AttackLeft)
        {
            // Checks if enemy player is on the left of the current player
            if ((currentPosition == CharacterPosition.Middle && enemyController.currentPosition == CharacterPosition.Right) ||
                (currentPosition == CharacterPosition.Right && enemyController.currentPosition == CharacterPosition.Middle))
            {
                enemyController.takeDamage(3);
            }
        }
        else if (currentAction == CharacterAction.AttackStraight)
        {
            // Checks if players are on the same tiles
            if ((currentPosition == CharacterPosition.Left && enemyController.currentPosition == CharacterPosition.Right) ||
                (currentPosition == CharacterPosition.Right && enemyController.currentPosition == CharacterPosition.Left) ||
                (currentPosition == CharacterPosition.Middle && enemyController.currentPosition == CharacterPosition.Middle))
            {
                enemyController.takeDamage(3);
            }
        }
        else if (currentAction == CharacterAction.AttackRight)
        {
            // Checks if enemy player is on the right of the current player
            if ((currentPosition == CharacterPosition.Middle && enemyController.currentPosition == CharacterPosition.Left) ||
                (currentPosition == CharacterPosition.Left && enemyController.currentPosition == CharacterPosition.Middle))
            {
                enemyController.takeDamage(3);
            }
        }
    }

    public void takeDamage(int dmg)
    {
        hp -= dmg;
        if (hp < 0)
        {
            hp = 0;
        }
    }
    #endregion Battle Damage Checks/Calculations

    #region Update Sprite Position
    public void moveSpritePosition()
    {
        if (currentAction == CharacterAction.MoveLeft)
        {
            transform.position += new Vector3(-2, 0, 0);
        }
        else if (currentAction == CharacterAction.MoveRight)
        {
            transform.position += new Vector3(2, 0, 0);
        }
    }
    #endregion Update Sprite Position

    #region Lock Move
    // Locks the player's move in for battle calculation
    public void lockMove()
    {
        moveLocked = true;
    }
    #endregion Lock Move

    #region Reset Turn
    // Resets players move and unlocks actions
    public void resetTurn()
    {
        moveLocked = false;
        currentAction = CharacterAction.None;
    }
    #endregion Reset Turn

    #region Obtain Protected Variables
    public CharacterPosition getCurrentPosition()
    {
        return currentPosition;
    }

    public CharacterAction getCurrentAction()
    {
        return currentAction;
    }
    #endregion Obtain Protected Variables
}
