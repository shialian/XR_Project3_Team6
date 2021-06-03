using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShowPlayerMP : MonoBehaviour
{
   

    private int MP = 100;
    private Text myText;
    private Text LackMPText;
    private Text CurrentMagicText;
    private Color TextColor;
    private int current_magic;
    private int i; //to control the speed of adding MP
    private Image MPBar;
   
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
    // Start is called before the first frame update

    void showLackMP()
    {
        TextColor.a = 1;
        LackMPText.color = TextColor;
    }


    void Start()
    {
        myText = (GameObject.Find("MP_text")).GetComponent<Text>();
        LackMPText = (GameObject.Find("lackMP")).GetComponent<Text>();
        CurrentMagicText = (GameObject.Find("CurrentMagic")).GetComponent<Text>();

        MPBar = (GameObject.Find("MPBar")).GetComponent<Image>();

        i = 0;
        TextColor = LackMPText.color;
        current_magic = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //get current MP
        int.TryParse(myText.text, out MP);

        //add MP
        i++; //to control the speed of adding MP
        if ( MP < 100 && i >= 3)
        {
            MP++;
            i = 0;
        }

        ////////////////////spell magic/////////////////
        //magic 1
       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            current_magic = 1;
            CurrentMagicText.text = "法術1";
        }

        //magic 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            current_magic = 2;
            CurrentMagicText.text = "法術2";
        }

        //magic 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            current_magic = 3;
            CurrentMagicText.text = "法術3";
        }

        //magic 4
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            current_magic = 4;
            CurrentMagicText.text = "法術4";
        }

        //shoot magic
        if (Input.GetMouseButtonDown(0))
        {
            ShootMagic(current_magic);
        }
        
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
