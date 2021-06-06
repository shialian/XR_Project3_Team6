using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseForTesting : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if(Physics.Raycast(ray, out hit, 100))
        {
            Debug.LogError(hit.collider);
        }
    }
}
