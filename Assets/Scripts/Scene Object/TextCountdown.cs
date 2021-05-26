using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCountdown : MonoBehaviour
{
    public string msg;
    public float countdown;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown >= 0)
        {
            int time = (int)Mathf.Ceil(countdown);
            text.SetText(msg + "\n" + time.ToString());
        }
    }
}
