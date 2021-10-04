﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ForbidenBlockTrigger : NetworkBehaviour
{
    public GameObject forbiddenBlock;

    public bool blockOn = false;

    private void Start()
    {
        forbiddenBlock.SetActive(false);
    }

    private void Update()
    {
        if (forbiddenBlock.activeSelf != blockOn)
        {
            forbiddenBlock.SetActive(blockOn);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && blockOn == false)
        {
            if (isLocalPlayer)
            {
                collision.gameObject.layer = LayerMask.NameToLayer("SpecificIgnoreInvisibleWarrior");
            }
            else
            {
                collision.gameObject.layer = LayerMask.NameToLayer("SpecificIgnoreWarrior");
            }
            foreach (Transform child in collision.transform.GetComponentsInChildren<Transform>())
            {
                if (child.name == "Hips" || child.name == "LichMesh")
                {
                    foreach (Transform c in child.GetComponentsInChildren<Transform>())
                    {
                        if (isLocalPlayer)
                        {
                            c.gameObject.layer = LayerMask.NameToLayer("SpecificIgnoreInvisibleWizzard");
                        }
                        else
                        {
                            c.gameObject.layer = LayerMask.NameToLayer("SpecificIgnoreWizzard");
                        }
                    }
                }
                if (child.name == "root" || child.name == "RPGHero")
                {
                    foreach (Transform c in child.GetComponentsInChildren<Transform>())
                    {
                        if (isLocalPlayer)
                        {
                            c.gameObject.layer = LayerMask.NameToLayer("SpecificIgnoreInvisibleWarrior");
                        }
                        else
                        {
                            c.gameObject.layer = LayerMask.NameToLayer("SpecificIgnoreWarrior");
                        }
                    }
                }
                if (child.name == "Wizzard")
                {
                    if (isLocalPlayer)
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("SpecificIgnoreInvisibleWizzard");
                    }
                    else
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("SpecificIgnoreWizzard");
                    }
                }
                if (child.name == "Colliders")
                {
                    foreach (Transform c in child.GetComponentsInChildren<Transform>())
                    {
                        c.gameObject.layer = LayerMask.NameToLayer("SpecificIgnore");
                    }
                }
            }
            blockOn = true;
        }
    }
}
