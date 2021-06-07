using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyExplainGameplay : MonoBehaviour
{
    private bool isExplain = false;
    public GameObject startMenu;
    public GameObject explainMenu;

    // Update is called once per frame
    void Update()
    {
        if (isExplain && Input.GetKeyDown(KeyCode.Escape))
        {
            isExplain = false;
            startMenu.SetActive(true);
            explainMenu.SetActive(false);
        }
    }

    public void ShowExplain()
    {
        isExplain = true;
        startMenu.SetActive(false);
        explainMenu.SetActive(true);
    }
}
