using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardMovement : MonoBehaviour
{
    public string vertical = "Vertical";
    public string horizontal = "Horizontal";
    public float speedFactor = 15f;

    private void Update()
    {
        Vector3 velocity = Vector3.zero;
        velocity.x = Input.GetAxis(horizontal);
        velocity.z = Input.GetAxis(vertical);
        transform.position += speedFactor * (velocity.z * transform.forward + velocity.x * transform.right) * Time.deltaTime;
    }
}
