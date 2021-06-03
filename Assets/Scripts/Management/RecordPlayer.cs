using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RecordPlayer : NetworkBehaviour
{
    private int playerID;
    private Ray ray;
    private RaycastHit hit;
    private bool updated;
    
    private void Start()
    {
        updated = false;
        ray = new Ray(transform.position, transform.up);
    }

    private void Update()
    {
        if (NetworkClient.ready && playerID == 0 && GameManager.singleton.localPlayerID != 0)
        {
            playerID = GameManager.singleton.localPlayerID;
        }
        if (updated == false && playerID != 0 && Physics.Raycast(ray, out hit, 10.0f))
        {
            Debug.LogError(hit.transform);
            if (hit.transform == NetworkClient.localPlayer.transform)
            {
                GameManager.singleton.UpdatePlayers(hit.transform.gameObject, playerID);
            }
            else
            {
                GameManager.singleton.UpdatePlayers(hit.transform.gameObject, 3 - playerID);
            }
            updated = true;
        }
    }
}
