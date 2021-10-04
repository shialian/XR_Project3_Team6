using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SelfInvisible : NetworkBehaviour
{
    private void Start()
    {
        if (isLocalPlayer)
        {
            transform.Find("RPGHero").gameObject.layer = LayerMask.NameToLayer("InvisibleWarrior");
            transform.Find("Wagon").Find("Wizzard").Find("LichMesh").gameObject.layer = LayerMask.NameToLayer("InvisibleWizzard");
        }
    }
}
