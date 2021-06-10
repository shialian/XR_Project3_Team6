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

    private WaitingRoomBGM SoundPlayer;

    private void Start()
    {
        playerOneReadyUI.SetActive(false);
        playerTwoReadyUI.SetActive(false);
        sceneIsLoaded = false;
        playerID = 0;

        SoundPlayer = (GameObject.Find("SoundPlayer")).GetComponent<WaitingRoomBGM>();

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
            SoundPlayer.CountDownSound();
            if (isServer && countdown.timer <= 0 && sceneIsLoaded == false)
            {
                sceneIsLoaded = true;
                GameManager.singleton.LoadScene(nextSceneName, 1);
            }
        }
        else if(hint.activeSelf == false)
        {
            SoundPlayer.Stop();
            hint.SetActive(true);
            countdown.gameObject.SetActive(false);
            countdown.timer = countdown.countdown;
        }
    }

    [Command(requiresAuthority = false)]
    public void SetPlayerReadyState(int id)
    {
        if(id == 1)
        {
            playerOneReady = playerOneReady ? false : true;
            PlaySound(playerOneReady);
        }
        else
        {
            playerTwoReady = playerTwoReady ? false : true;
            PlaySound(playerTwoReady);
        }
    }

    [ClientRpc]
    public void PlaySound(bool ready)
    {
        if (ready)
        {
            SoundPlayer.ReadySound();
        }
        else
        {
            SoundPlayer.CancelReadySound();
        }
    }

    private void SetPlayerReadyUI()
    {
        if (playerOneReady)
        {
            //SoundPlayer.ReadySound();
            playerOneReadyUI.SetActive(true);
        }
        else
        {
            //SoundPlayer.CancelReadySound();
            playerOneReadyUI.SetActive(false);
        }
        if (playerTwoReady)
        {
            //SoundPlayer.ReadySound();
            playerTwoReadyUI.SetActive(true);
        }
        else
        {
            //SoundPlayer.CancelReadySound();
            playerTwoReadyUI.SetActive(false);
        }
    }

    private bool AllPlayerAreReady()
    {
        return playerOneReady && playerTwoReady;
    }
}
