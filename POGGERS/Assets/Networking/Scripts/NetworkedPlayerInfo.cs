using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayerInfo : NetworkBehaviour{

    public PlayerController playerController;

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
    }

    [Command]
    public void CmdUpdatePosition(CharacterPosition position)
    {
        characterPosition = position;
    }
}
