using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShootController : MonoBehaviour
{
    public TimeAreaManager timeAreaManager_pause;
    public TimeAreaManager timeAreaManager_speedUp;
    public TimeAreaManager timeAreaManager_slowDown;
    public TimeAreaManager timeAreaManager_backward;
    public bool lockMouse;

    public MyTimeState shootType = MyTimeState.Pause;

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


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootType = MyTimeState.Pause;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootType = MyTimeState.SpeedUp;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shootType = MyTimeState.SlowDown;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            shootType = MyTimeState.BackWard;
        }
    }

    private void ShootTheSkill()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10000f);
        if (hit.collider != null && hit.collider.tag == "Plane")
        {
            switch (shootType)
            {
                case MyTimeState.Pause:
                    timeAreaManager_pause.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
                    break;
                case MyTimeState.SpeedUp:
                    timeAreaManager_speedUp.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
                    break;
                case MyTimeState.SlowDown:
                    timeAreaManager_slowDown.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
                    break;
                case MyTimeState.BackWard:
                    timeAreaManager_backward.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
                    break;
            }
            //timeAreaManager.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
        }
    }
}
