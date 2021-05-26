using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WaitingRoomUIInformation : NetworkBehaviour
{
    public GameObject countdown;
    public GameObject playerOneReadyUI;
    public GameObject playerTwoReadyUI;
    public GameObject hint;
    public string nextSceneName = "Play Scene Features Test";

    [SyncVar]
    public bool playerOneReady = false;
    [SyncVar]
    public bool playerTwoReady = false;

    private GameObject localPlayer = null;

    private int playerID;

    private void Start()
    {
        playerOneReadyUI.SetActive(false);
        playerTwoReadyUI.SetActive(false);
    }

    private void Update()
    {
        if(NetworkClient.ready && localPlayer == null)
        {
            SetLocalPlayer();
        }
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A))
        {
            SetPlayerReadyState(playerID);
        }
        SetPlayerReadyUI();
        if (AllPlayerAreReady()) {
            hint.SetActive(false);
            countdown.SetActive(true);
            if (isServer)
            {
                StartCoroutine(SendLoadScene(nextSceneName, 5.0f));
            }
        }
    }

    private void SetLocalPlayer()
    {
        localPlayer = NetworkClient.localPlayer.gameObject;
        if (Vector3.Distance(localPlayer.transform.position, playerOneReadyUI.transform.position) < Vector3.Distance(localPlayer.transform.position, playerTwoReadyUI.transform.position))
        {
            playerID = 1;
        }
        else
        {
            playerID = 2;
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

    private IEnumerator SendLoadScene(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.singleton.LoadScene(sceneName);
    }
}
