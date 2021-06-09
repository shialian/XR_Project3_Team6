using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    public void Return()
    {
        GameManager.singleton.LoadScene("ReadyRoom", 0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
