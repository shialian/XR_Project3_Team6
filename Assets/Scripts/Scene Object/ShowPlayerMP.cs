using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShowPlayerMP : MonoBehaviour
{
    /* 子UI */
    private Transform skillUI;
    private Transform MPUI;

    private float MP ;
    private TextMeshProUGUI myText;
    private TextMeshProUGUI LackMPText;
    private TextMeshProUGUI CurrentMagicText;
    private Color TextColor;
    private int current_magic;
   // private int i; //to control the speed of adding MP
    private Image MPBar;

    //magic image
    private GameObject SpeedUpMagic;
    private GameObject SlowDownMagic;
    private GameObject StopMagic;
    private GameObject RewindMagic;
    private GameObject Bomb;

    private MyShootController myShootController;

    public void showLackMP()
    {
        TextColor.a = 1;
        LackMPText.color = TextColor;
    }

    public void HideMagicImage(int magic_number)
    {
        switch (magic_number)
        {
            case 1:
                StopMagic.SetActive(false);
                break;
            case 2:
                SpeedUpMagic.SetActive(false);
                break;
            case 3:
                SlowDownMagic.SetActive(false);
                break;
            case 4:
                RewindMagic.SetActive(false);
                break;
            case 5:
                Bomb.SetActive(false);
                break;
            default:
                break;

        }
    }

    public void ShowMagicImage(int magic_number)
    {
        switch (magic_number)
        {
            case 1:
                StopMagic.SetActive(true);
                break;
            case 2:
                SpeedUpMagic.SetActive(true);
                break;
            case 3:
                SlowDownMagic.SetActive(true);
                break;
            case 4:
                RewindMagic.SetActive(true);
                break;
            case 5:
                if (GameManager.singleton.bongs[GameManager.singleton.localPlayerID - 1] > 0)
                {
                    Bomb.SetActive(true);
                }
                break;
            default:
                break;
        }
    }

    public void ChangeMagicText(int magic_number)
    {
        switch (magic_number)
        {
            case 1:
                CurrentMagicText.SetText("暫停");
                break;
            case 2:
                CurrentMagicText.SetText("快轉區域");
                break;
            case 3:
                CurrentMagicText.SetText("緩慢區域");
                break;
            case 4:
                CurrentMagicText.SetText("倒轉");
                break;
            case 5:
                CurrentMagicText.SetText("炸彈");
                break;
        }
    }

    void Start()
    {
        skillUI = transform.Find("Skill");
        MPUI = transform.Find("MP");

        CurrentMagicText = skillUI.Find("CurrentMagic").GetComponent<TextMeshProUGUI>();
        SpeedUpMagic = skillUI.Find("SpeedUPMagic").gameObject;
        SlowDownMagic = skillUI.Find("SlowDownMagic").gameObject;
        StopMagic = skillUI.Find("StopMagic").gameObject;
        RewindMagic = skillUI.Find("RewindMagic").gameObject;
        Bomb = skillUI.Find("Bomb").gameObject;

        myText = MPUI.Find("MP_text").GetComponent<TextMeshProUGUI>();
        MPBar = MPUI.Find("MPBar").GetComponent<Image>();

        myShootController = transform.parent.GetComponent<MyShootController>();
        MP = myShootController.GetMP();
        LackMPText = transform.Find("lackMP").GetComponent<TextMeshProUGUI>();

        StopMagic.SetActive(false);
        SpeedUpMagic.SetActive(false);
        SlowDownMagic.SetActive(false);
        RewindMagic.SetActive(false);
        Bomb.SetActive(false);

        TextColor = LackMPText.color;
        TextColor.a = 0f;
        LackMPText.color = TextColor;

        HideMagicImage(current_magic);
        current_magic = (int)myShootController.shootType;
        ChangeMagicText(current_magic);
        ShowMagicImage(current_magic);
    }

    // Update is called once per frame
    void Update()
    {
        MP = myShootController.GetMP();
        /*
        //get current MP
        int.TryParse(myText.text, out MP);

        //add MP
        i++; //to control the speed of adding MP
        if ( MP < 100 && i >= 3)
        {
            MP++;
            i = 0;
        }
        */
        ////////////////////spell magic/////////////////
        //magic 1
       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HideMagicImage(current_magic);
            current_magic = 1;
            ChangeMagicText(current_magic);
            ShowMagicImage(current_magic);
        }

        //magic 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HideMagicImage(current_magic);
            current_magic = 2;
            ChangeMagicText(current_magic);
            ShowMagicImage(current_magic);
        }

        //magic 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HideMagicImage(current_magic);
            current_magic = 3;
            ChangeMagicText(current_magic);
            ShowMagicImage(current_magic);
        }

        //magic 4
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HideMagicImage(current_magic);
            current_magic = 4;
            ChangeMagicText(current_magic);
            ShowMagicImage(current_magic);
        }

        //Bomb
        if (Input.GetKeyDown(KeyCode.R))
        {
            HideMagicImage(current_magic);
            current_magic = 5;
            ChangeMagicText(current_magic);
            ShowMagicImage(current_magic);
        }


        //shoot magic
        /*
        if (Input.GetMouseButtonDown(0))
        {
            ShootMagic(current_magic);
        }*/

        //MP不足的字    
        TextColor = LackMPText.color;
        if( TextColor.a > 0)
        {
            TextColor.a -= 0.01f;
            LackMPText.color = TextColor;
        }

        myText.SetText(MP.ToString());


        // MP Bar 調整
        MPBar.fillAmount = (float)MP / 100;
    }
}
