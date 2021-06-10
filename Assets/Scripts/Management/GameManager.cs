using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton = null;

    /* 場景物件跟回合紀錄 */
    public GameObject cookie = null;
    public int round = 1;

    /* 玩家資料紀錄 */
    [SyncVar]
    public int numPlayer = 0;
    public int localPlayerID;
    [SyncVar]
    public bool playerGetCookie = false;

    /* 獲勝條件 */
    public int winCondition = 3;
    [SyncVar]
    public bool playerWinTheGame = false;
    [SyncVar]
    public int winnerID = 0;
    
    /* 其他以list形式儲存的資料 */
    public SyncList<bool> throwed = new SyncList<bool>();
    public SyncList<GameObject> players = new SyncList<GameObject>();
    public SyncList<int> getCookies = new SyncList<int>();
    public SyncList<int> bongs = new SyncList<int>();

    private bool newRoundInvokeing;

    private void Start()
    {
        localPlayerID = 0;
        if (isServer)
        {
            getCookies.Add(0);
            getCookies.Add(0);
            bongs.Add(1);
            bongs.Add(1);
            throwed.Add(false);
            throwed.Add(false);
        }
        newRoundInvokeing = false;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        if (NetworkClient.ready && localPlayerID == 0)
        {
            SetLocalPlayerID();
            NewPlayerAdd();
        }
        if(SceneManager.GetActiveScene().name == "Play Scene" && cookie == null)
        {
            cookie = GameObject.Find("cookie").gameObject;
        }
        if(cookie && cookie.activeSelf == playerGetCookie)
        {
            cookie.SetActive(!playerGetCookie);
        }
        if(isServer)
        {
            CheckPlayerGetCookies();
            if ((PlayerThrowed() || playerGetCookie) && newRoundInvokeing == false && playerWinTheGame == false)
            {
                newRoundInvokeing = true;
                if (playerGetCookie)
                {
                    Invoke("NewRoundStart", 2.5f);
                    Invoke("ResetBongs", 2.5f);
                }
                else
                {
                    NewRoundStart();
                    ResetBongs();
                }
                ResetThrowed();
            }
        }
    }

    public void SetLocalPlayerID()
    {
        localPlayerID = numPlayer + 1;
    }

    [Command(requiresAuthority = false)]
    public void NewPlayerAdd()
    {
        numPlayer++;
    }

    private bool PlayerThrowed()
    {
        return (throwed[0] == true) && (throwed[1] == true);
    }

    [Command(requiresAuthority = false)]
    public void UpdatePlayers(GameObject player, int id)
    {
        if (players.Count < id)
        {
            players.Add(player);
        }
        else
        {
            players[id - 1] = player;
        }
    }

    [Command(requiresAuthority = false)]
    public void GetTheCookie(int id)
    {
        Debug.LogError(id);
        getCookies[id - 1]++;
        playerGetCookie = true;
    }

    [ClientRpc]
    public void NewRoundStart()
    {
        round++;
        for (int i=0; i< 2; i++) {
            Transform warrior = players[i].transform;
            //Transform warrior = players[localPlayerID - 1].transform;
            Transform pcCamera = warrior.transform.Find("Wagon").Find("Wizzard").Find("PC Camera");
            Transform wizzard = warrior.transform.Find("Wagon").Find("Wizzard");
            wizzard.GetComponent<GetCookieOrNot>().ResetAll();
            warrior.GetComponent<MyMovement>().enabled = true;
            pcCamera.GetComponent<MyShootController>().enabled = true;
            pcCamera.GetComponent<MyShootController>().shootType = MyTimeState.SpeedUp;
            cookie.SetActive(true);
            newRoundInvokeing = false;
        }
    }

    private void ResetBongs()
    {
        bongs[0] = bongs[1] = 1;
    }

    private void CheckPlayerGetCookies()
    {
        Debug.LogError(getCookies[1]);
        if(getCookies[0] == winCondition)
        {
            winnerID = 1;
            playerWinTheGame = true;
            Time.timeScale = 0;
        }
        else if(getCookies[1] == winCondition)
        {
            winnerID = 2;
            playerWinTheGame = true;
            Time.timeScale = 0;
        }
    }

    [Command(requiresAuthority = false)]
    public void HasThrowed(int id)
    {
        throwed[id - 1] = true;
    }

    [Command(requiresAuthority = false)]
    public void ResetThrowed()
    {
        throwed[0] = false;
        throwed[1] = false;
    }

    [Command(requiresAuthority =  false)]
    public void ResetGetCookie()
    {
        playerGetCookie = false;
    }

    [Command(requiresAuthority = false)]
    public void ComsumeBong(int id)
    {
        bongs[id - 1]--;
    }

    [Command(requiresAuthority = false)]
    public void LoadScene(string sceneName, int spawnID)
    {
        ConnectionManager.cmSingleton.ChangeSpawnID(spawnID);
        ConnectionManager.cmSingleton.ServerChangeScene(sceneName);
    }

    [Command(requiresAuthority = false)]
    public void NewGameStart()
    {
        getCookies[0] = getCookies[1] = 0;
        bongs[0] = bongs[1] = 0;
        throwed[0] = throwed[1] = false;
        newRoundInvokeing = false;
    }
}
