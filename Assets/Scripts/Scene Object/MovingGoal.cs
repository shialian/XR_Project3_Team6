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
            Debug.LogError("here!");
            Transform warrior = collision.transform;
            if (warrior.GetComponent<Movement>() != null)
            {
                warrior.GetComponent<Movement>().enabled = false;
            }
            if (warrior.GetComponent<MyMovement>() != null)
            {
                warrior.GetComponent<MyMovement>().enabled = false;
            }
            warrior.GetComponent<Throw>().enabled = true;
            warrior.GetComponent<Throw>().Start();
            warrior.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            throwOn = true;
        }
    }
}
