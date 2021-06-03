using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton = null;

    [HideInInspector, SyncVar]
    public int numPlayer = 0;
    [HideInInspector]
    public int localPlayerID;
    public SyncList<GameObject> players = new SyncList<GameObject>();
    public SyncList<int> getCookies = new SyncList<int>();

    private int prevNumPlayer;

    private void Start()
    {
        localPlayerID = 0;
        prevNumPlayer = numPlayer;
        if (isServer)
        {
            getCookies.Add(0);
            getCookies.Add(0);
        }
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        if (NetworkClient.ready && localPlayerID == 0 && numPlayer != prevNumPlayer)
        {
            SetLocalPlayerID();
        }
        if(NetworkClient.ready && numPlayer == prevNumPlayer)
        {
            NewPlayerAdd();
        }
        
    }

    [Command(requiresAuthority = false)]
    public void NewPlayerAdd()
    {
        numPlayer++;
    }

    public void SetLocalPlayerID()
    {
        localPlayerID = numPlayer;
    }

    [Command(requiresAuthority = false)]
    public void UpdatePlayers(GameObject player, int id)
    {
        if(players.Count < id)
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
        getCookies[id - 1]++;
    }

    public void LoadScene(string sceneName, int spawnID)
    {
        ConnectionManager.cmSingleton.ChangeSpawnID(spawnID);
        ConnectionManager.cmSingleton.ServerChangeScene(sceneName);
    }
}
