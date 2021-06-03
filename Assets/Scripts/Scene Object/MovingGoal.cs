using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGoal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
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
            warrior.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
