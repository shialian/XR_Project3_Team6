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

    public float timeAreaManager_pause_consuming;
    public float timeAreaManager_speedUp_consuming;
    public float timeAreaManager_slowDown_consuming;
    public float timeAreaManager_backward_consuming;
    public float timeBoxManager_pause_consuming;
    public float timeBoxManager_speedUp_consuming;
    public float timeBoxManager_slowDown_consuming;

    public TimeAreaManager timeBoxManager_bong;

    public bool lockMouse;

    public MyTimeState shootType = MyTimeState.Pause;

    public float maxMagic;
    public float currentMagic;
    public float magicRecoverySpeed;

    private void Start()
    {
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        currentMagic = maxMagic;
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

        if (currentMagic < maxMagic)
        {
            currentMagic += magicRecoverySpeed * Time.deltaTime;
        }else
        {
            currentMagic = maxMagic;
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
                    if (currentMagic >= timeAreaManager_pause_consuming) {
                        currentMagic -= timeAreaManager_pause_consuming;
                        timeAreaManager_pause.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
                    }
                    break;
                case MyTimeState.SpeedUp:
                    if (currentMagic >= timeAreaManager_speedUp_consuming)
                    {
                        currentMagic -= timeAreaManager_speedUp_consuming;
                        timeAreaManager_speedUp.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
                    }
                    break;
                case MyTimeState.SlowDown:
                    if (currentMagic >= timeAreaManager_slowDown_consuming)
                    {
                        currentMagic -= timeAreaManager_slowDown_consuming;
                        timeAreaManager_slowDown.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
                    }
                    break;
                case MyTimeState.BackWard:
                    if (currentMagic >= timeAreaManager_backward_consuming)
                    {
                        currentMagic -= timeAreaManager_backward_consuming;
                        timeAreaManager_backward.CreateTimeArea(transform.position, Quaternion.identity, hit.point);
                    }
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
                    if (currentMagic >= timeBoxManager_pause_consuming)
                    {
                        currentMagic -= timeBoxManager_pause_consuming;
                        timeBoxManager_pause.CreateTimeArea(hit.point, Quaternion.identity, hit.point);
                    }
                    break;
                case MyTimeState.SpeedUp:
                    if (currentMagic >= timeBoxManager_speedUp_consuming)
                    {
                        currentMagic -= timeBoxManager_speedUp_consuming;
                        timeBoxManager_speedUp.CreateTimeArea(hit.point, Quaternion.identity, hit.point);
                    }
                    break;
                case MyTimeState.SlowDown:
                    if (currentMagic >= timeBoxManager_slowDown_consuming)
                    {
                        currentMagic -= timeBoxManager_slowDown_consuming;
                        timeBoxManager_slowDown.CreateTimeArea(hit.point, Quaternion.identity, hit.point);
                    }
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
