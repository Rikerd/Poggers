using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour {
    protected int hp;

    protected int atk;

    // Prevents futher action if turn is complete until reset by the BattleManager
    protected bool moveLocked;

    // Where the player is (left, middle, right)
    [SerializeField]
    protected CharacterPosition currentPosition;

    // What the player is doing for the turn
    [SerializeField]
    protected CharacterAction currentAction;

    protected SpriteRenderer sprite;

    #region Battle Damage Checks/Calculations
    // Opposite checks because position is realtive to model POV
    public void performAttack(CharacterController enemyController)
    {
        if (enemyController.currentAction == CharacterAction.Block)
        {
            return;
        }

        if (currentAction == CharacterAction.AttackLeft)
        {
            // Checks if enemy player is on the left of the current player
            if ((currentPosition == CharacterPosition.Middle && enemyController.currentPosition == CharacterPosition.Right) ||
                (currentPosition == CharacterPosition.Right && enemyController.currentPosition == CharacterPosition.Middle))
            {
                enemyController.takeDamage(atk);
            }
        }
        else if (currentAction == CharacterAction.AttackStraight)
        {
            // Checks if players are on the same tiles
            if ((currentPosition == CharacterPosition.Left && enemyController.currentPosition == CharacterPosition.Right) ||
                (currentPosition == CharacterPosition.Right && enemyController.currentPosition == CharacterPosition.Left) ||
                (currentPosition == CharacterPosition.Middle && enemyController.currentPosition == CharacterPosition.Middle))
            {
                enemyController.takeDamage(atk);
            }
        }
        else if (currentAction == CharacterAction.AttackRight)
        {
            // Checks if enemy player is on the right of the current player
            if ((currentPosition == CharacterPosition.Middle && enemyController.currentPosition == CharacterPosition.Left) ||
                (currentPosition == CharacterPosition.Left && enemyController.currentPosition == CharacterPosition.Middle))
            {
                enemyController.takeDamage(atk);
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

    public bool isDead()
    {
        return hp <= 0;
    }
    #endregion Battle Damage Checks/Calculations

    #region Update Position
    // SET ROTATION Y FOR OPPOSING SIDE TO 180 TO MOVE PROPERLY
    public void movePosition()
    {
        // Checks which movement and properly updates
        if (currentAction == CharacterAction.MoveLeft)
        {
            transform.position -= transform.right * 2;

            // Checks and change to proper placement
            if (currentPosition == CharacterPosition.Middle)
            {
                currentPosition = CharacterPosition.Left;
            }
            else if (currentPosition == CharacterPosition.Right)
            {
                currentPosition = CharacterPosition.Middle;
            }
        }
        else if (currentAction == CharacterAction.MoveRight)
        {
            transform.position += transform.right * 2;

            // Checks and change to proper placement
            if (currentPosition == CharacterPosition.Middle)
            {
                currentPosition = CharacterPosition.Right;
            }
            else if (currentPosition == CharacterPosition.Left)
            {
                currentPosition = CharacterPosition.Middle;
            }
        }
    }
    #endregion Update Position

    #region Update Postion V2
    public void movePositonTwo()
    {
        int positionMultiplier = 1;
        int yPosition = 0;

        if (transform.rotation.eulerAngles.x == 180)
        {
            positionMultiplier = -1;
            yPosition = 2;
        }
        if (currentPosition == CharacterPosition.Left)
        {
            transform.position = new Vector3(-2 * positionMultiplier, yPosition);
        } else if (currentPosition == CharacterPosition.Middle)
        {
            transform.position = new Vector3(0, 0);
        } else if (currentPosition == CharacterPosition.Right)
        {
            transform.position = new Vector3(2 * positionMultiplier, yPosition);
        }
    }
    #endregion Update Postion V2

    #region Update Sprite Color
    public void changeSpriteColor()
    {
        if (currentAction == CharacterAction.Block)
        {
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = Color.white;
        }
    }
    #endregion Update Sprite Color

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
        sprite.color = Color.white;
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

    public string getCurrentActionString()
    {
        if (currentAction == CharacterAction.AttackLeft)
        {
            return "Left Attack";
        }
        else if (currentAction == CharacterAction.AttackStraight)
        {
            return "Straight Attack";
        }
        else if (currentAction == CharacterAction.AttackRight)
        {
            return "Right Attack";
        }
        else if (currentAction == CharacterAction.MoveLeft)
        {
            return "Move Left";
        }
        else if (currentAction == CharacterAction.MoveRight)
        {
            return "Move Right";
        }
        else if (currentAction == CharacterAction.Block)
        {
            return "Block";
        }
        else
        {
            return "None";
        }
    }

    public int getHp()
    {
        return hp;
    }
    #endregion Obtain Protected Variables
}
