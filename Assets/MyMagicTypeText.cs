using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMagicTypeText : MonoBehaviour
{
    public GameObject[] magicText;
    public MyShootController shootController;
    

    // Update is called once per frame
    void Update()
    {
        MyTimeState state = shootController.shootType;
        switch (state)
        {
            case MyTimeState.Pause:
                magicText[0].SetActive(true);
                magicText[1].SetActive(false);
                magicText[2].SetActive(false);
                magicText[3].SetActive(false);
                magicText[4].SetActive(true);
                break;
            case MyTimeState.SpeedUp:
                magicText[0].SetActive(false);
                magicText[1].SetActive(true);
                magicText[2].SetActive(false);
                magicText[3].SetActive(false);
                magicText[4].SetActive(true);
                break;
            case MyTimeState.SlowDown:
                magicText[0].SetActive(false);
                magicText[1].SetActive(false);
                magicText[2].SetActive(true);
                magicText[3].SetActive(false);
                magicText[4].SetActive(true);
                break;
            case MyTimeState.BackWard:
                magicText[0].SetActive(false);
                magicText[1].SetActive(false);
                magicText[2].SetActive(false);
                magicText[3].SetActive(true);
                magicText[4].SetActive(true);
                break;
            case MyTimeState.Normal:
                magicText[0].SetActive(false);
                magicText[1].SetActive(false);
                magicText[2].SetActive(false);
                magicText[3].SetActive(false);
                magicText[4].SetActive(false);
                break;
        }
    }
}
