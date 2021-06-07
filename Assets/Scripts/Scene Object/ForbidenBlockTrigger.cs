﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ForbidenBlockTrigger : NetworkBehaviour
{
    public GameObject forbiddenBlock;

    [SyncVar]
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
            foreach (Transform child in collision.transform.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("SpecificIgnore");
            }
            SetBlockOn();
        }
    }

    [Command(requiresAuthority = false)]
    public void SetBlockOn()
    {
        blockOn = true;
    }

    [Command(requiresAuthority = false)]
    public void CloseBlock()
    {
        blockOn = false;
    }
}
