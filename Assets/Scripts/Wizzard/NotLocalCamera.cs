using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NotLocalCamera : NetworkBehaviour
{
    public GameObject cam;
    public GameObject OVRCam;

    private void Update()
    {
        if(!isLocalPlayer)
        {
            cam.SetActive(false);
            OVRCam.SetActive(false);
            this.enabled = false;
        }
    }
}
