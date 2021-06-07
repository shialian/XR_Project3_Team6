using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGoal : MonoBehaviour
{
    public bool throwOn = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" && throwOn == false)
        {
            int playerID = GameManager.singleton.localPlayerID;
            if (collision.gameObject == GameManager.singleton.players[playerID - 1])
            {
                Transform warrior = collision.transform;
                warrior.GetComponent<MyMovement>().ResetAnimator();
                warrior.GetComponent<MyMovement>().enabled = false;
                warrior.GetComponent<Throw>().enabled = true;
                warrior.GetComponent<Throw>().Start();
                warrior.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                warrior.Find("Wagon").Find("Wizzard").GetComponent<GetCookieOrNot>().enabled = true;
                throwOn = true;
            }
        }
    }
}
