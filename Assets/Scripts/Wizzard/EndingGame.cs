using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingGame : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    private int playerID;

    private void Start()
    {
        playerID = 0;
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.singleton) {
            if(playerID == 0)
            {
                playerID = GameManager.singleton.localPlayerID;
            }
            if (GameManager.singleton.playerWinTheGame)
            {
                Cursor.lockState = CursorLockMode.None;
                if (GameManager.singleton.winnerID == playerID)
                {
                    winUI.SetActive(true);
                }
                else
                {
                    loseUI.SetActive(true);
                }
        }
        }
    }
}
