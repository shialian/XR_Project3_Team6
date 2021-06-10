using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomBGM : MonoBehaviour
{
    public AudioClip CancelReady;
    public AudioClip Ready;
    public AudioClip CountDown;
    public AudioClip BtnClick;
    public AudioClip PageFlip;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReadySound()
    {
        audioSource.PlayOneShot(Ready);

    }

    public void Stop()
    {
        audioSource.Stop();

    }

    public void CancelReadySound()
    {
        audioSource.PlayOneShot(CancelReady);


    }

    public void CountDownSound()
    {

        audioSource.PlayOneShot(CountDown, 0.5f);

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
