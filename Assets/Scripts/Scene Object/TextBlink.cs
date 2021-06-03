using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    private Text myText;
    private Color showColor;
    private Color hideColor;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        showColor = myText.color;
        hideColor = showColor;
        hideColor.a = 0f;
        print(hideColor);
    }

    // Update is called once per frame
    void Update()
    {
        myText.color = Color.Lerp(showColor, hideColor, Mathf.PingPong(Time.time*1.5f, 1));

    }
}
