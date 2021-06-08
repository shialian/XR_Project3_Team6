using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCookieOrNot : MonoBehaviour
{
    public Transform[] startPosition;
    public GameObject cookieOnHand;

    private Animator anim;
    private int playerID;
    private Transform warrior;

    private void Awake()
    {
        playerID = 0;
        startPosition = new Transform[2];
        warrior = transform.parent.parent;
        anim = GetComponent<Animator>();
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
            cookieOnHand.SetActive(true);
            anim.SetBool("Win", true);
            Invoke("ResetAll", 2.5f);
        }
        else if(collision.collider.tag != "Platform")
        {
            ResetAll();
        }
        else
        {
            GetComponent<WizzardMovement>().enabled = true;
        }
        GameManager.singleton.HasThrowed(playerID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
        {
            // play drop down water sound here!
            Invoke("ResetAll", 2.5f);
            GameManager.singleton.HasThrowed(playerID);
        }
    }

    private void ResetAll()
    {
        cookieOnHand.SetActive(false);
        anim.SetBool("Win", false);
        GameManager.singleton.ResetGetCookie();
        ResetWizzard(transform);
        ResetWarrior(warrior);
        ResetGoal();
        ResetForbiddenBlock();
    }

    private void ResetWizzard(Transform wizzard)
    {
        Rigidbody rb = wizzard.GetComponent<Rigidbody>();
        wizzard.GetComponent<WizzardMovement>().enabled = false;
        wizzard.GetComponent<WizzardRotation>().cameraY = 90f;
        wizzard.localPosition = new Vector3(-0.25f, 1.5f, 0.0f);
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        wizzard.Find("PC Camera").GetComponent<MyShootController>().enabled = false;
    }

    private void ResetWarrior(Transform warrior)
    {
        warrior.position = startPosition[playerID - 1].position;
        warrior.rotation = startPosition[playerID - 1].rotation;
        warrior.GetComponent<Throw>().ResetAll();
        warrior.GetComponent<Throw>().enabled = false;
        foreach(Transform child in warrior.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    private void ResetGoal()
    {
        GameObject.Find("Goal").GetComponent<MovingGoal>().throwOn = false;
    }

    public void ResetForbiddenBlock()
    {
        GameObject stair = GameObject.Find("Clider_stair");
        stair.GetComponent<ForbidenBlockTrigger>().blockOn = false;
    }
}
