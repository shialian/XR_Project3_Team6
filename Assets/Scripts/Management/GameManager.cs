﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public static GameManager singleton = null;

    [SyncVar]
    public int numPlayer = 0;
    [SyncVar]
    public bool playerGetCookie = false;
    public int round = 1;
    public int localPlayerID;
    public SyncList<bool> throwed = new SyncList<bool>();
    public SyncList<GameObject> players = new SyncList<GameObject>();
    public SyncList<int> getCookies = new SyncList<int>();
    public SyncList<int> bongs = new SyncList<int>();

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
        if(isServer && (PlayerThrowed() || playerGetCookie))
        {
            NewRoundStart();
            ResetThrowed();
            ResetGetCookie();
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
        getCookies[id - 1]++;
    }

    [ClientRpc]
    public void NewRoundStart()
    {
        round++;
        Transform warrior = players[localPlayerID - 1].transform;
        Transform pcCamera = warrior.transform.Find("Wagon").Find("Wizzard").Find("PC Camera");
        warrior.GetComponent<MyMovement>().enabled = true;
        pcCamera.GetComponent<MyShootController>().enabled = true;
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

    public void LoadScene(string sceneName, int spawnID)
    {
        ConnectionManager.cmSingleton.ChangeSpawnID(spawnID);
        ConnectionManager.cmSingleton.ServerChangeScene(sceneName);
    }
}
