using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public TimeAreaManager timeAreaManager;
    public bool lockMouse;

    private void Start()
    {
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootTheSkill();
        }
    }

    private void ShootTheSkill()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10000f);
        if(hit.collider != null && hit.collider.tag == "Plane")
        {
            timeAreaManager.CreateTimeAreaByServerCalling(transform.position, Quaternion.identity, hit.point);
        }
    }
}
