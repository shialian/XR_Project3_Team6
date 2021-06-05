using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCookieOrNot : MonoBehaviour
{
    public Transform[] startPosition;

    private int playerID;

    private void Awake()
    {
        playerID = 0;
        startPosition = new Transform[2];
    }

    private void Update()
    {
        if(GameManager.singleton && playerID == 0)
        {
            playerID = GameManager.singleton.localPlayerID;
        }
        if (startPosition[0] == null)
        {
            startPosition[0] = GameObject.Find("Player 1 Spawn Position").transform;
            startPosition[1] = GameObject.Find("Player 2 Spawn Position").transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Cookie")
        {
            GameManager.singleton.GetTheCookie(playerID);
        }
        ResetWizzard(transform);
        ResetWarrior(transform.parent.parent);
        ResetGoal();
    }

    private void ResetWizzard(Transform wizzard)
    {
        Rigidbody rb = wizzard.GetComponent<Rigidbody>();
        wizzard.localPosition = new Vector3(-0.25f, 1.5f, 0.0f);
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void ResetWarrior(Transform warrior)
    {
        warrior.position = startPosition[playerID - 1].position;
        warrior.rotation = startPosition[playerID - 1].rotation;
        warrior.GetComponent<Throw>().ResetAll();
        warrior.GetComponent<Throw>().enabled = false;
        warrior.GetComponent<MyMovement>().enabled = true;
    }

    private void ResetGoal()
    {
        GameObject.Find("Goal").GetComponent<MovingGoal>().throwOn = false;
    }
}
