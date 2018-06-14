using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerInfo : NetworkBehaviour{

    public PlayerController2 playerController;

    [SyncVar]
    public CharacterAction characterAction;
    [SyncVar]
    public CharacterPosition characterPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;
        CmdUpdateAction(playerController.getCurrentAction());
        CmdUpdatePosition(playerController.getCurrentPosition());
        characterAction = playerController.getCurrentAction();
        characterPosition = playerController.getCurrentPosition();
        print(playerController.getCurrentAction());
	}

    public void getInfo()
    {
        if (!isLocalPlayer)
            return;
        characterAction = playerController.getCurrentAction();
        characterPosition = playerController.getCurrentPosition();
    }

    [Command]
    public void CmdUpdateAction(CharacterAction action)
    {
        characterAction = action;
        print(netId  + " did " + action);
    }

    [Command]
    public void CmdUpdatePosition(CharacterPosition position)
    {
        characterPosition = position;
        print(netId + " in " + position);
    }


    public string getCurrentActionString()
    {
        if (characterAction == CharacterAction.AttackLeft)
        {
            return "Left Attack";
        }
        else if (characterAction == CharacterAction.AttackStraight)
        {
            return "Straight Attack";
        }
        else if (characterAction == CharacterAction.AttackRight)
        {
            return "Right Attack";
        }
        else if (characterAction == CharacterAction.MoveLeft)
        {
            return "Move Left";
        }
        else if (characterAction == CharacterAction.MoveRight)
        {
            return "Move Right";
        }
        else if (characterAction == CharacterAction.Block)
        {
            return "Block";
        }
        else
        {
            return "None";
        }
    }
}
