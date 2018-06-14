using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManagerNetworked : NetworkBehaviour {
    // First index is character controlled player
    // Second index is enemy
    public CharacterController[] controllers;
    public GameObject[] networkedPlayers;

    // UI Variables
    public Text playerHpUI;
    public Text enemyHpUI;
    public Text timerUI;
    public Text actionUI;
    public GameObject gameOverPanel;
    public Text gameOverPrompter;

    // Time game waits for player to perform move
    public float actionSelectTimer;

    // Time game takes to perform the move before waiting for next move
    public float performActionTimer;

    // Timer UI Stats
    private float timer;
    private bool activateTimer;

    // Use this for initialization
    void Start()
    {
        timer = actionSelectTimer;
        StartCoroutine(BattleSystem());

        #region Networked Players Setup
        networkedPlayers = GameObject.FindGameObjectsWithTag("Player");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Networked Players Setup
        if (networkedPlayers.Length != 2)
        {
            networkedPlayers = GameObject.FindGameObjectsWithTag("Player");
            return;
        }
        #endregion

        #region Networked Player Updates
        for (int i = 0; i < 2; i++)
        {
            NetworkedPlayerInfo playerinfo = networkedPlayers[i].GetComponent<NetworkedPlayerInfo>();
            NetworkedPlayerController playerSprite;
            if (playerinfo.isLocalPlayer)
            {
                playerSprite = controllers[0].GetComponent<NetworkedPlayerController>();
            }
            else
            {
                networkedPlayers[i].GetComponent<PlayerController2>().enabled = false;
                networkedPlayers[i].GetComponent<BoxCollider2D>().enabled = false;
                playerSprite = controllers[1].GetComponent<NetworkedPlayerController>();
            }

            playerinfo.getInfo();
            playerSprite.PullInfo(playerinfo);
        }
        #endregion

        #region UI Updates
        // Updates the UI with proper information
        playerHpUI.text = "Your HP: " + controllers[0].getHp();
        enemyHpUI.text = "Enemy HP: " + controllers[1].getHp();

        if (networkedPlayers[0].GetComponent<NetworkedPlayerInfo>().isLocalPlayer)
            //actionUI.text = "Current Action: " + controllers[0].getCurrentActionString();
            actionUI.text = "Current Action: " + networkedPlayers[0].GetComponent<NetworkedPlayerInfo>().getCurrentActionString();
        else ///networkedPlayers[1].GetComponent<NetworkedPlayerInfo>().isLocalPlayer
            //actionUI.text = "Current Action: " + controllers[1].getCurrentActionString();
            actionUI.text = "Current Action: " + networkedPlayers[1].GetComponent<NetworkedPlayerInfo>().getCurrentActionString();


        if (activateTimer)
        {
            if (timer - Time.deltaTime < 0)
            {
                timer = 0;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        timerUI.text = timer.ToString("F2");
        #endregion UI Updates
    }

    IEnumerator BattleSystem()
    {
        while (true)
        {
            #region Activate Timer

            activateTimer = true;
            timer = actionSelectTimer;

            #endregion Activate Timer

            // Waits for action for the turn from each player
            yield return new WaitForSeconds(actionSelectTimer);

            #region Deactivate Timer

            activateTimer = false;
            timer = 0f;

            #endregion Deactivate Timer

            // Locks each player's move after
            foreach (CharacterController controller in controllers)
            {
                controller.lockMove();
            }

            // Waits before preforming move
            yield return new WaitForSeconds(performActionTimer / 2);

            #region Action Phase
            // Updates Players First
            for (int i = 0; i < 2; i++)
            {
                NetworkedPlayerInfo playerinfo = networkedPlayers[i].GetComponent<NetworkedPlayerInfo>();
                NetworkedPlayerController playerSprite;
                if (playerinfo.isLocalPlayer)
                    playerSprite = controllers[0].GetComponent<NetworkedPlayerController>();
                else
                    playerSprite = controllers[1].GetComponent<NetworkedPlayerController>();

                playerinfo.getInfo();
                playerSprite.PullInfo(playerinfo);
            }
            // Preforms Movement First
            foreach (GameObject player in networkedPlayers)
            {
                player.GetComponent<CharacterController>().movePosition();
                player.GetComponent<CharacterController>().movePositonTwo();
            }
            foreach (CharacterController controller in controllers)
            {
                controller.movePosition();
                controller.movePositonTwo();
                controller.changeSpriteColor();
            }

            // Checks attacks for both players
            controllers[0].performAttack(controllers[1]);
            controllers[1].performAttack(controllers[0]);
            #endregion Action Phase

            // Waits before reseting turns
            yield return new WaitForSeconds(performActionTimer / 2);

            #region Post Action Phase
            // Checks if a player is dead
            if (controllers[0].isDead() || controllers[1].isDead())
            {
                activateTimer = false;

                gameOverPanel.SetActive(true);

                if (controllers[0].isDead() && controllers[1].isDead())
                {
                    // Both sides died at the same time
                    gameOverPrompter.text = "TIE!";
                }
                else if (controllers[0].isDead())
                {
                    // Controlling player died
                    gameOverPrompter.text = "YOU DIED!";
                }
                else if (controllers[1].isDead())
                {
                    // Enemy died
                    gameOverPrompter.text = "YOU WIN!";
                }

                //StopCoroutine(coroutine);
                break;
            }

            // Resets Turns
            foreach (CharacterController controller in controllers)
            {
                controller.resetTurn();
            }
            foreach (GameObject player in networkedPlayers)
            {
                player.GetComponent<CharacterController>().resetTurn();
            }
            #endregion Post Action Phase
        }
    }
}
