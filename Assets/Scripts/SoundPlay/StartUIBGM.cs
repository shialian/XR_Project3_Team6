using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIBGM : MonoBehaviour
{
    public AudioClip BtnClick;
    public AudioClip PageFlip;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBtnClickSound()
    {
        audioSource.PlayOneShot(BtnClick, 0.7F);

    }

    public void PlayPageFlipSound()
    {
        audioSource.PlayOneShot(PageFlip, 0.7F);

    }

   
   
}
