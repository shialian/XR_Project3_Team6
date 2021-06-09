using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowPlayerRank : MonoBehaviour
{

    private Transform RankUI;

    private int LocalID;
    private int AnotherID;

    private Vector3 P1_position;
    private Vector3 P2_position;
    
    private GameObject First;
    private GameObject Second;

    private Text temp;
    // Start is called before the first frame update
    void Start()
    {
        RankUI = transform.Find("Rank");
        First = RankUI.Find("First").gameObject;
        Second = RankUI.Find("Second").gameObject;

        LocalID = 0;
    }
  

    // Update is called once per frame
    void Update()
    {
        if(GameManager.singleton && LocalID == 0)
        {
            LocalID = GameManager.singleton.localPlayerID;
            if (LocalID == 1)
            {
                AnotherID = 2;
            }
            else
            {
                AnotherID = 1;
            }
        }
        if (LocalID != 0 && PlayerReady())
        {
            P1_position = GameManager.singleton.players[LocalID - 1].transform.position;
            P2_position = GameManager.singleton.players[AnotherID - 1].transform.position;
            if (P1_position.z >= P2_position.z)
            {
                First.SetActive(true);
                Second.SetActive(false);

            }
            else
            {
                First.SetActive(false);
                Second.SetActive(true);
            }
        }
    }

    private bool PlayerReady()
    {
        for(int i = 0; i < GameManager.singleton.players.Count; i++)
        {
            if (GameManager.singleton.players[i] == null)
                return false;
        }
        return true;
    }
}
