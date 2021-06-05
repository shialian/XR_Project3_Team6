using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PCCamera : NetworkBehaviour
{
    public GameObject camera;

    private void Update()
    {
        if(!isLocalPlayer)
        {
            camera.SetActive(false);
            this.enabled = false;
        }
    }
}
