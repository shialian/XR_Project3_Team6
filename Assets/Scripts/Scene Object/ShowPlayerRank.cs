using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowPlayerRank : MonoBehaviour
{

    private GameManager gameManager;
  
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

        gameManager = (GameObject.Find("GameManager")).GetComponent<GameManager>();
        First = GameObject.Find("First");
        Second = GameObject.Find("Second");

        LocalID = gameManager.localPlayerID;
        if (LocalID == 1)
        {
            AnotherID = 2;
        }
        else
        {
            AnotherID = 1;
        }
    }
  

    // Update is called once per frame
    void Update()
    {
        P1_position = gameManager.players[LocalID - 1].transform.position;
        P2_position = gameManager.players[AnotherID - 1].transform.position;
        if( P1_position.z >= P2_position.z)
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
