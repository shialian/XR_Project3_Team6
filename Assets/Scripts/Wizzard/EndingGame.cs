using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingGame : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    private PlaySceneSound SoundPlayer;

    private int playerID;

    private void Start()
    {
        playerID = 0;
        winUI.SetActive(false);
        loseUI.SetActive(false);
        SoundPlayer = (GameObject.Find("SoundPlayer")).GetComponent<PlaySceneSound>();

    }

    private void Update()
    {
        if (GameManager.singleton) {
            if(playerID == 0)
            {
                playerID = GameManager.singleton.localPlayerID;
            }
            if (GameManager.singleton.playerWinTheGame && UIOn())
            {
                Cursor.lockState = CursorLockMode.None;
                if (GameManager.singleton.winnerID == playerID)
                {
                    SoundPlayer.WinSound();
                    winUI.SetActive(true);
                }
                else
                {
                    SoundPlayer.LoseSound();
                    loseUI.SetActive(true);
                }
        }
        }
    }

    private bool UIOn()
    {
        return (winUI.activeSelf == false) && (loseUI.activeSelf == false);
    }
}
