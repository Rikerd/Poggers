using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The place the player is in order to determine what moves are valid
public enum CharacterPosition
{
    Left,
    Middle,
    Right
}

// The action that a player decides to do for that "turn"
public enum CharacterAction
{
    None,
    AttackLeft,
    AttackStraight,
    AttackRight,
    MoveLeft,
    MoveRight,
    Block
}
