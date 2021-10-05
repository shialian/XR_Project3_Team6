using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MyShootController : MonoBehaviour
{
    public TimeAreaManager timeAreaManager_pause;
    public TimeAreaManager timeAreaManager_backward;
    public TimeAreaManager timeBoxManager_speedUp;
    public TimeAreaManager timeBoxManager_slowDown;

    public float timeAreaManager_pause_consuming;
    public float timeAreaManager_backward_consuming;
    public float timeBoxManager_speedUp_consuming;
    public float timeBoxManager_slowDown_consuming;

    public TimeAreaManager timeBoxManager_bong;

    public bool lockMouse;

    public MyTimeState shootType = MyTimeState.SpeedUp;

    public float maxMagic;
    public float currentMagic;
    public float magicRecoverySpeed;

    public GameObject bongImage;

    private MyMovement movement;

    private PlaySceneSound SoundPlayer;

    private ShowPlayerMP showPlayerMP;

    private void Start()
    {
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        SoundPlayer = (GameObject.Find("SoundPlayer")).GetComponent<PlaySceneSound>();
        //showPlayerMP = (GameObject.Find("SceneManager")).GetComponent<ShowPlayerMP>();
        showPlayerMP = transform.Find("Player_UI").GetComponent<ShowPlayerMP>();

        currentMagic = maxMagic;
        timeAreaManager_pause = GameObject.Find("Time Trigger Pool (Stop)").GetComponent<TimeAreaManager>();
        timeAreaManager_backward = GameObject.Find("Time Trigger Pool (Backword)").GetComponent<TimeAreaManager>();
        timeBoxManager_speedUp = GameObject.Find("Time Area Pool (SpeedUp)").GetComponent<TimeAreaManager>();
        timeBoxManager_slowDown = GameObject.Find("Time Area Pool (SlowDown)").GetComponent<TimeAreaManager>();
        timeBoxManager_bong = GameObject.Find("Time Trigger Pool (Bong)").GetComponent<TimeAreaManager>();
        movement = transform.parent.parent.parent.GetComponent<MyMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (shootType == MyTimeState.Pause || shootType == MyTimeState.BackWard)
                ShootTheSkill();
            else if (shootType == MyTimeState.SlowDown || shootType == MyTimeState.SpeedUp)
                SetTheSkill();
            else if (shootType == MyTimeState.Normal)
            {
                int playerID = GameManager.singleton.localPlayerID;
                if (GameManager.singleton.bongs[playerID - 1] > 0)
                {
                    SoundPlayer.ShootMagic(5);
                    SetBong();
                    GameManager.singleton.ComsumeBong(playerID);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shootType = MyTimeState.Pause;
            SoundPlayer.ChangeMagicSound();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shootType = MyTimeState.SpeedUp;
            SoundPlayer.ChangeMagicSound();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shootType = MyTimeState.SlowDown;
            SoundPlayer.ChangeMagicSound();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            shootType = MyTimeState.BackWard;
            SoundPlayer.ChangeMagicSound();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            shootType = MyTimeState.Normal;
            SoundPlayer.ChangeMagicSound();
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
            float velocity = movement.velocity;
            switch (shootType)
            {
                case MyTimeState.Pause:
                    if (currentMagic >= timeAreaManager_pause_consuming) {
                        SoundPlayer.ShootMagic(1);
                        currentMagic -= timeAreaManager_pause_consuming;
                        timeAreaManager_pause.CreateTimeAreaByServerCalling(transform.position, Quaternion.identity, hit.point, velocity);
                    }
                    else
                    {
                        showPlayerMP.showLackMP();
                    }
                    break;
                /*
                case MyTimeState.SpeedUp:
                    if (currentMagic >= timeAreaManager_speedUp_consuming)
                    {
                        currentMagic -= timeAreaManager_speedUp_consuming;
                        timeAreaManager_speedUp.CreateTimeAreaByServerCalling(transform.position, Quaternion.identity, hit.point, velocity);
                    }
                    break;
                case MyTimeState.SlowDown:
                    if (currentMagic >= timeAreaManager_slowDown_consuming)
                    {
                        currentMagic -= timeAreaManager_slowDown_consuming;
                        timeAreaManager_slowDown.CreateTimeAreaByServerCalling(transform.position, Quaternion.identity, hit.point, velocity);
                    }
                    break;
                */
                case MyTimeState.BackWard:
                    if (currentMagic >= timeAreaManager_backward_consuming)
                    {
                        SoundPlayer.ShootMagic(4);
                        currentMagic -= timeAreaManager_backward_consuming;
                        timeAreaManager_backward.CreateTimeAreaByServerCalling(transform.position, Quaternion.identity, hit.point, velocity);
                    }
                    else
                    {
                        showPlayerMP.showLackMP();
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
            float velocity = movement.velocity;
            switch (shootType)
            {   
                /*
                case MyTimeState.Pause:
                    if (currentMagic >= timeBoxManager_pause_consuming)
                    {
                        currentMagic -= timeBoxManager_pause_consuming;
                        timeBoxManager_pause.CreateTimeAreaByServerCalling(hit.point, Quaternion.identity, hit.point, velocity);
                    }
                    break;
                */
                case MyTimeState.SpeedUp:
                    if (currentMagic >= timeBoxManager_speedUp_consuming)
                    {
                        SoundPlayer.ShootMagic(2);
                        currentMagic -= timeBoxManager_speedUp_consuming;
                        timeBoxManager_speedUp.CreateTimeAreaByServerCalling(hit.point, Quaternion.identity, hit.point, velocity);
                    }
                    else
                    {
                        showPlayerMP.showLackMP();
                    }
                    break;
                case MyTimeState.SlowDown:
                    if (currentMagic >= timeBoxManager_slowDown_consuming)
                    {
                        SoundPlayer.ShootMagic(3);
                        currentMagic -= timeBoxManager_slowDown_consuming;
                        timeBoxManager_slowDown.CreateTimeAreaByServerCalling(hit.point, Quaternion.identity, hit.point, velocity);
                    }
                    else
                    {
                        showPlayerMP.showLackMP();
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
            float velocity = movement.velocity;
            timeBoxManager_bong.CreateTimeAreaByServerCalling(transform.position, Quaternion.identity, hit.point, velocity);
            bongImage.SetActive(false);
        }
    }

    public int GetMP()
    {
        return (int)currentMagic;
    }
}
