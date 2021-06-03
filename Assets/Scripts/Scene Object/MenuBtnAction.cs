using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//
public class MenuBtnAction : MonoBehaviour
{
    public GameObject Start_btn;
    public GameObject Exit_btn;
    public GameObject HowToPlay_btn;
    public GameObject TTS_btn; // TTS = touch to start
    public GameObject TTS_text;

    public void clickStart()
    {
        SceneManager.LoadScene(1);
    }
    public void clickExit()
    {
        Debug.Log("exit");
        Application.Quit();
    }

    public void clickTTS()
    {
        TTS_btn.SetActive(false);
        TTS_text.SetActive(false);
        Start_btn.SetActive(true);
        Exit_btn.SetActive(true);
        HowToPlay_btn.SetActive(true);

    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}