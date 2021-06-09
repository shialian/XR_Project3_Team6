using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    private PlaySceneSound SoundPlayer;

    private bool isPlay;
    private float timer;

    private float Playtime;
    void Start()
    {
        SoundPlayer = (GameObject.Find("SoundPlayer")).GetComponent<PlaySceneSound>();
        timer = 1.0f;
        isPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - Playtime >= timer)
            isPlay = false;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Tree")
        {
            if (isPlay == false)
            {
                SoundPlayer.WoodCollisionSound();
                Playtime = Time.time;
                isPlay = true;
            }
        }
        else if (collision.gameObject.tag == "Stone")
        {
            if (isPlay == false)
            {
                SoundPlayer.StoneCollisionSound();
                Playtime = Time.time;
                isPlay = true;
            }
        }


    }
}
