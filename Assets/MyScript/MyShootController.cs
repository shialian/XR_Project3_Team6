using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShootController : MonoBehaviour
{
    public TimeAreaManager timeAreaManager_pause;
    public TimeAreaManager timeAreaManager_speedUp;
    public TimeAreaManager timeAreaManager_slowDown;
    public TimeAreaManager timeAreaManager_backward;

    public TimeAreaManager timeBoxManager_pause;
    public TimeAreaManager timeBoxManager_speedUp;
    public TimeAreaManager timeBoxManager_slowDown;

    public TimeAreaManager timeBoxManager_bong;

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftShift))
        {
            ShootTheSkill();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftShift))
        {
            SetTheSkill();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && Input.GetKey(KeyCode.LeftShift))
        {
            SetBong();
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            shootType = MyTimeState.Normal;
        }
    }

    private void ShootTheSkill()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10000f,  LayerMask.GetMask("Plane"));
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
                case MyTimeState.Normal:
                    break;
            }
            //timeAreaManager.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
        }
    }

    private void SetTheSkill()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10000f, LayerMask.GetMask("Plane"));
        if (hit.collider != null && hit.collider.tag == "Plane")
        {
            switch (shootType)
            {
                case MyTimeState.Pause:
                    timeBoxManager_pause.CreateTimeArea(hit.point, Quaternion.identity, hit.point);
                    break;
                case MyTimeState.SpeedUp:
                    timeBoxManager_speedUp.CreateTimeArea(hit.point, Quaternion.identity, hit.point);
                    break;
                case MyTimeState.SlowDown:
                    timeBoxManager_slowDown.CreateTimeArea(hit.point, Quaternion.identity, hit.point);
                    break;
              
            }
            //timeAreaManager.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
        }
    }

    private void SetBong()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10000f, LayerMask.GetMask("Plane"));
        if (hit.collider != null && hit.collider.tag == "Plane")
        {
            timeBoxManager_bong.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
        }
    }


}
