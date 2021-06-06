using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PCCamera : NetworkBehaviour
{
    public GameObject cam;

    private void Update()
    {
        if(!isLocalPlayer)
        {
            cam.SetActive(false);
            this.enabled = false;
        }
    }
}
