using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowPlayerRank : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    private Vector3 P1_position;
    private Vector3 P2_position;
    
    private GameObject First;
    private GameObject Second;

    private Text temp;
    // Start is called before the first frame update
    void Start()
    {
        First = GameObject.Find("First");
        Second = GameObject.Find("Second");
        
    }
  

    // Update is called once per frame
    void Update()
    {
        P1_position = Player1.transform.position;
        P2_position = Player2.transform.position;
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
