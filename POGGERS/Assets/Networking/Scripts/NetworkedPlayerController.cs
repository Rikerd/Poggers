using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedPlayerController : CharacterController
{
    /// <summary>
    /// This version is basically a dumby version so that the game manager
    /// could move the sprite around as it gets the data from the
    /// player prefab that is spawned by the network
    /// </summary>

    // Use this for initialization
    void Start()
    {
        hp = 3;
        atk = 1;

        currentPosition = CharacterPosition.Middle;
        currentAction = CharacterAction.None;

        sprite = GetComponent<SpriteRenderer>();

        moveLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PullInfo(NetworkedPlayerInfo info)
    {
        currentAction = info.characterAction;
        currentPosition = info.characterPosition;
    }

    public void PullInfo(PlayerController actualPlayer)
    {
        currentAction = actualPlayer.getCurrentAction();
        currentPosition = actualPlayer.getCurrentPosition();
    }
}
