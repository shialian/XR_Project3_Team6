using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForbidenBlockTrigger : MonoBehaviour
{
    public GameObject forbidenBlock;

    [HideInInspector]
    public bool blockOn;

    private void Start()
    {
        forbidenBlock.SetActive(false);
        blockOn = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && blockOn == false)
        {
            forbidenBlock.SetActive(true);
            foreach(Transform child in collision.transform.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("SpecificIgnore");
            }
            blockOn = true;
        }
    }
}
