using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomBGM : MonoBehaviour
{
    public AudioClip CancelReady;
    public AudioClip Ready;
    public AudioClip CountDown;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReadySound()
    {
        audioSource.PlayOneShot(Ready, 0.7F);

    }

    public void Stop()
    {
        audioSource.Stop();

    }

    public void CancelReadySound()
    {
        audioSource.PlayOneShot(CancelReady, 0.7F);


    }

    public void CountDownSound()
    {

        audioSource.PlayOneShot(CountDown, 0.7F);

    }
}
