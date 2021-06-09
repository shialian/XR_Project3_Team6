using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    private GameObject TTS; // touch to start
    // Start is called before the first frame update
    void Start()
    {
        TTS = GameObject.Find("TTS_Btn");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
            ExecuteEvents.Execute(TTS, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }
    }
}
