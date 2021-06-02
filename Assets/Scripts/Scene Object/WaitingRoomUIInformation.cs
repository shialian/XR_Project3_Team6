using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaitingRoomUIInformation : NetworkBehaviour
{
    public TextCountdown countdown;
    public GameObject playerOneReadyUI;
    public GameObject playerTwoReadyUI;
    public GameObject hint;
    public string nextSceneName = "Play Scene Features Test";

    [SyncVar]
    public bool playerOneReady = false;
    [SyncVar]
    public bool playerTwoReady = false;

    private int playerID;
    private bool sceneIsLoaded;

    private void Start()
    {
        playerOneReadyUI.SetActive(false);
        playerTwoReadyUI.SetActive(false);
        sceneIsLoaded = false;
        playerID = 0;
    }

    private void Update()
    {
        // Get player ID when client is ready
        if(NetworkClient.ready && playerID == 0)
        {
            playerID = GameManager.singleton.localPlayerID;
        }

        // Input event
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
        {
            SetPlayerReadyState(playerID);
        }

        // Player ready
        SetPlayerReadyUI();
        if (AllPlayerAreReady()) {
            hint.SetActive(false);
            countdown.gameObject.SetActive(true);
            if (isServer && countdown.timer <= 0 && sceneIsLoaded == false)
            {
                sceneIsLoaded = true;
                GameManager.singleton.LoadScene(nextSceneName, 1);
            }
        }
        else
        {
            hint.SetActive(true);
            countdown.gameObject.SetActive(false);
            countdown.timer = countdown.countdown;
        }
    }

    [Command(requiresAuthority = false)]
    private void SetPlayerReadyState(int id)
    {
        if(id == 1)
        {
            playerOneReady = playerOneReady ? false : true;
        }
        else
        {
            playerTwoReady = playerTwoReady ? false : true;
        }
    }

    private void SetPlayerReadyUI()
    {
        if (playerOneReady)
        {
            playerOneReadyUI.SetActive(true);
        }
        else
        {
            playerOneReadyUI.SetActive(false);
        }
        if (playerTwoReady)
        {
            playerTwoReadyUI.SetActive(true);
        }
        else
        {
            playerTwoReadyUI.SetActive(false);
        }
    }

    private bool AllPlayerAreReady()
    {
        return playerOneReady && playerTwoReady;
    }
}
