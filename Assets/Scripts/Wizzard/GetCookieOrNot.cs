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

    public bool endGame;


    private void Awake()
    {
        playerID = 0;
        startPosition = new Transform[2];
        warrior = transform.parent.parent;
        anim = GetComponent<Animator>();
        endGame = false;
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
        if (warrior.GetComponent<MyMovement>().isLocalPlayer)
        {
            if (collision.collider.tag == "Cookie")
            {
                if (GameManager.singleton.getCookies[playerID - 1] + 1 == GameManager.singleton.winCondition)
                {
                    endGame = true;
                }
                GameManager.singleton.GetTheCookie(playerID);
                cookieOnHand.SetActive(true);
                anim.SetBool("Win", true);
                if (endGame == false)
                {
                    Invoke("ResetAll", 2.5f);
                }
                GameManager.singleton.HasThrowed(playerID);
            }
            else if (collision.collider.tag == "Wagon")
            {
                ResetAll();
            }
            else if (collision.collider.tag == "Platform")
            {
                GetComponent<WizzardMovement>().enabled = true;
                GameManager.singleton.HasThrowed(playerID);
            }
            Debug.LogError(collision.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (warrior.GetComponent<MyMovement>().isLocalPlayer) {
            if (other.tag == "Water")
            {
                // play drop down water sound here!
                Invoke("ResetAll", 2.5f);
                GameManager.singleton.HasThrowed(playerID);
            }
        }
    }

    public void ResetAll()
    {
        cookieOnHand.SetActive(false);
        anim.SetBool("Win", false);
        GameManager.singleton.ResetGetCookie();
        ResetWarrior(warrior);
        ResetWizzard(transform);
        ResetGoal();
        //ResetForbiddenBlock();
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
        wizzard.gameObject.layer = LayerMask.NameToLayer("Wizzard");
        foreach (Transform child in wizzard.GetComponentsInChildren<Transform>())
        {
            if (child.name == "Hips" || child.name == "LichMesh")
            {
                foreach (Transform c in child.GetComponentsInChildren<Transform>())
                {
                    c.gameObject.layer = LayerMask.NameToLayer("Wizzard");
                }
            }
        }
    }

    private void ResetWarrior(Transform warrior)
    {
        warrior.position = startPosition[playerID - 1].position;
        warrior.rotation = startPosition[playerID - 1].rotation;
        warrior.GetComponent<Throw>().ResetAll();
        warrior.GetComponent<Throw>().enabled = false;
        warrior.gameObject.layer = LayerMask.NameToLayer("Warrior");
        foreach (Transform child in warrior.GetComponentsInChildren<Transform>())
        {
            if (child.name == "root" || child.name == "RPGHero")
            {
                foreach (Transform c in child.GetComponentsInChildren<Transform>())
                {
                    c.gameObject.layer = LayerMask.NameToLayer("Warrior");
                }
            }
            if (child.name == "Colliders")
            {
                foreach (Transform c in child.GetComponentsInChildren<Transform>())
                {
                    c.gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
    }

    private void ResetGoal()
    {
        GameObject.Find("Goal").GetComponent<MovingGoal>().throwOn = false;
    }

    public void ResetForbiddenBlock()
    {
        Debug.LogError("Here");
        GameObject stair = GameObject.Find("Clider_stair");
        stair.GetComponent<ForbidenBlockTrigger>().blockOn = false;
    }
}
