using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    // First index is character controlled player
    // Second index is enemy
    public CharacterController[] controllers;

    public Text playerHpUI;
    public Text enemyHpUI;
    public Text timerUI;
    public Text actionUI;
    
    // Time game waits for player to perform move
    public float actionSelectTimer;
    
    // Time game takes to perform the move before waiting for next move
    public float performActionTimer;

    // Timer UI Stats
    private float timer;
    private bool activateTimer;

	// Use this for initialization
	void Start () {
        timer = actionSelectTimer;
        StartCoroutine(BattleSystem());
	}
	
	// Update is called once per frame
	void Update () {
        playerHpUI.text = "Your HP: " + controllers[0].getHp();
        enemyHpUI.text = "Enemy HP: " + controllers[1].getHp();
        actionUI.text = "Current Action: " + controllers[0].getCurrentActionString();

        if (activateTimer)
        {
            if (timer < 0)
            {
                timer = 0;
            } else
            {
                timer -= Time.deltaTime;
            }
        }

        timerUI.text = timer.ToString("F2");
	}

    IEnumerator BattleSystem()
    {
        while (true)
        {
            activateTimer = true;
            timer = actionSelectTimer;

            // Waits for action for the turn from each player
            yield return new WaitForSeconds(actionSelectTimer);

            activateTimer = false;
            timer = 0f;

            // Locks each player after
            foreach (CharacterController controller in controllers)
            {
                controller.lockMove();
            }

            // Waits before preforming move
            yield return new WaitForSeconds(performActionTimer / 2);

            // Preforms Movement First
            foreach (CharacterController controller in controllers)
            {
                controller.movePosition();
                controller.changeSpriteColor();
            }

            // Checks attacks for both players
            controllers[0].performAttack(controllers[1]);
            controllers[1].performAttack(controllers[0]);

            // Waits before reseting turns
            yield return new WaitForSeconds(performActionTimer / 2);


            // Resets Turns
            foreach (CharacterController controller in controllers)
            {
                controller.resetTurn();
            }
        }
    }
}
