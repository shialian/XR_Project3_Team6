using System.Collections;
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
        //if (forbiddenBlock.activeSelf != blockOn)
        //{
        //    forbiddenBlock.SetActive(blockOn);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && blockOn == false)
        {
            foreach(Transform child in collision.transform.GetComponentsInChildren<Transform>())
            {
                if (GetCollider(child))
                {
                    child.gameObject.layer = LayerMask.NameToLayer("SpecificIgnore");
                }
            }
            blockOn = true;
            CmdBlockOnAndOff(blockOn);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdBlockOnAndOff(bool flag)
    {
        RpcBlockOnAndOff(flag);
    }

    [ClientRpc]
    public void RpcBlockOnAndOff(bool flag)
    {
        forbiddenBlock.SetActive(flag);
    }

    private bool GetCollider(Transform transform)
    {
        if (transform.GetComponent<BoxCollider>())
        {
            return true;
        }
        if (transform.GetComponent<CapsuleCollider>())
        {
            return true;
        }
        if (transform.GetComponent<MeshCollider>())
        {
            return true;
        }
        if (transform.GetComponent<SphereCollider>())
        {
            return true;
        }
        return false;
    }
}
