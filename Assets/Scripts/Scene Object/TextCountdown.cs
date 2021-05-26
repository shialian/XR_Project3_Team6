using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCountdown : MonoBehaviour
{
    public string msg;
    public float countdown;

    [HideInInspector]
    public float timer;

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        timer = countdown;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer >= 0)
        {
            int time = (int)Mathf.Ceil(timer);
            text.SetText(msg + "\n" + time.ToString());
        }
    }
}
