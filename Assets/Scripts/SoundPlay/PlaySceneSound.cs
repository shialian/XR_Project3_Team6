using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneSound : MonoBehaviour
{   
    // Magic Related
    public AudioClip LackMP;
    public AudioClip ChangeMagic;
    public AudioClip ShootMagicSpeedup;
    public AudioClip ShootMagicSlowDown;
    public AudioClip ShootMagicStop;
    public AudioClip ShootMagicRewind;
    public AudioClip ShootMagicBomb;

    //Shot Related
    public AudioClip Speedup_shot;
    public AudioClip SlowDown_shot;
    public AudioClip Stop_shot;
    public AudioClip Rewind_shot;
    public AudioClip Bomb_shot;

    // Move Related
    public AudioClip Run;
    public AudioClip WoodCollision;
    public AudioClip StoneCollision;

    // Thorw Relate
    public AudioClip Throw;
    public AudioClip IntoWater;


    //Result Relate
    public AudioClip GetCookie;
    public AudioClip Win;
    public AudioClip Lose;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }
    
    // Magic Related
    public void LackMpSound()
    {
        audioSource.PlayOneShot(LackMP);

    }
    public void ChangeMagicSound()
    {
        audioSource.PlayOneShot(ChangeMagic);


    }
    public void ShootMagic(int magic_number)
    {
        switch (magic_number)
        {
            case 1:
                audioSource.PlayOneShot(ShootMagicStop);
                break;
            case 2:
                audioSource.PlayOneShot(ShootMagicSpeedup);
                break;
            case 3:
                audioSource.PlayOneShot(ShootMagicSlowDown);
                break;
            case 4:
                audioSource.PlayOneShot(ShootMagicRewind);
                break;
            case 5:
                audioSource.PlayOneShot(ShootMagicBomb);
                break;
            default:
                break;

        }
    }

    // Get shot
    public void GetShot(int magic_number)
    {
        switch (magic_number)
        {
            case 1:
                audioSource.PlayOneShot(Stop_shot);
                break;
            case 2:
                audioSource.PlayOneShot(Speedup_shot);
                break;
            case 3:
                audioSource.PlayOneShot(SlowDown_shot);
                break;
            case 4:
                audioSource.PlayOneShot(Rewind_shot);
                break;
            
            case 5:
                audioSource.PlayOneShot(Bomb_shot,0.7f);
                break;
            
            default:
                break;

        }

    }

    // Move related
    public void RunSound()
    {
        audioSource.PlayOneShot(Run);
    }
    public void WoodCollisionSound()
    {
        audioSource.PlayOneShot(WoodCollision);
    }
    public void StoneCollisionSound()
    {
        audioSource.PlayOneShot(StoneCollision);
    }

    // Throw Related
    public void ThrowSound()
    {
        audioSource.PlayOneShot(Throw);
    }
    public void IntoWaterSound()
    {
        audioSource.PlayOneShot(IntoWater);
    }
   



    public void Stop()
    {
        audioSource.Stop();

    }
}
