using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardMovement : MonoBehaviour
{
    public string vertical = "Vertical";
    public string horizontal = "Horizontal";
    public float speedFactor = 15f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 velocity = Vector3.zero;
        velocity.x = Input.GetAxis(horizontal);
        velocity.z = Input.GetAxis(vertical);
        transform.position += speedFactor * (velocity.z * transform.forward + velocity.x * transform.right) * Time.deltaTime;
        anim.SetFloat("Speed", velocity.sqrMagnitude);
    }
}
