using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShowPlayerMP : MonoBehaviour
{
   

    private float MP ;
    private Text myText;
    private Text LackMPText;
    private Text CurrentMagicText;
    private Color TextColor;
    private int current_magic;
    private int i; //to control the speed of adding MP
    private Image MPBar;

    //magic image
    private GameObject SpeedUpMagic;
    private GameObject SlowDownMagic;
    private GameObject StopMagic;
    private GameObject RewindMagic;
    private GameObject Bomb;

    private MyShootController myShootController;


    /*
    void ShootMagic(int magic_number)
    {
        switch (magic_number)
        {
            case 1:
                if (MP >= 30)
                {
                    MP -= 30;
                    Debug.Log("法術1");

                }
                else
                {
                    showLackMP();
                }
                break;
            case 2:
                if (MP >= 30)
                {
                    MP -= 30;
                    Debug.Log("法術2");

                }
                else
                {
                    showLackMP();
                }
                break;
            case 3:
                if (MP >= 25)
                {
                    MP -= 25;
                    Debug.Log("法術3");

                }
                else
                {
                    showLackMP();
                }
                break;
            case 4:
                if (MP >= 75)
                {
                    MP -= 75;
                    Debug.Log("法術4");

                }
                else
                {
                    showLackMP();
                }
                break;
            default:
                break;

        }

    }
    */
    // Start is called before the first frame update

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
                Bomb.SetActive(true);
                break;
            default:
                break;

        }


    }



    void Start()
    {
        myText = (GameObject.Find("MP_text")).GetComponent<Text>();
        LackMPText = (GameObject.Find("lackMP")).GetComponent<Text>();
        CurrentMagicText = (GameObject.Find("CurrentMagic")).GetComponent<Text>();

        MPBar = (GameObject.Find("MPBar")).GetComponent<Image>();

        myShootController = (GameObject.Find("PC Camera")).GetComponent<MyShootController>();
        MP = myShootController.GetMP();

        SpeedUpMagic = GameObject.Find("SpeedUPMagic");
        SlowDownMagic = GameObject.Find("SlowDownMagic");
        StopMagic = GameObject.Find("StopMagic");
        RewindMagic = GameObject.Find("RewindMagic");
        Bomb = GameObject.Find("Bomb");

        StopMagic.SetActive(true);
        SpeedUpMagic.SetActive(false);
        SlowDownMagic.SetActive(false);
        RewindMagic.SetActive(false);
        Bomb.SetActive(false);

        i = 0;
        TextColor = LackMPText.color;
        current_magic = 1;
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
            CurrentMagicText.text = "暫停";
            StopMagic.SetActive(true);
        }

        //magic 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HideMagicImage(current_magic);
            current_magic = 2;
            CurrentMagicText.text = "快轉區域";
            SpeedUpMagic.SetActive(true);
        }

        //magic 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HideMagicImage(current_magic);
            current_magic = 3;
            CurrentMagicText.text = "緩慢區域";
            SlowDownMagic.SetActive(true);
        }

        //magic 4
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HideMagicImage(current_magic);
            current_magic = 4;
            CurrentMagicText.text = "倒轉";
            RewindMagic.SetActive(true);
        }

        //Bomb
        if (Input.GetKeyDown(KeyCode.R))
        {
            HideMagicImage(current_magic);
            current_magic = 5;
            CurrentMagicText.text = "炸彈";
            Bomb.SetActive(true);
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


        myText.text  = MP.ToString();


        // MP Bar 調整
        MPBar.fillAmount = (float)MP / 100;
    }
}
