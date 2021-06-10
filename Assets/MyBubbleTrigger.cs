using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBubbleTrigger : MonoBehaviour
{
    public bool canTrigger = true;

    public MyTimeState effectType;
    public float effectTimeLength;

    public GameObject bubbleVFX;

    public bool isBong = false;
    public bool isDestroyAfterTrigger = false;

    private PlaySceneSound SoundPlayer;
    private int playerID;

    public void Start()
    {
        canTrigger = true;
        playerID = 0;
        SoundPlayer = (GameObject.Find("SoundPlayer")).GetComponent<PlaySceneSound>();
    }

    private void Update()
    {
        if(GameManager.singleton && playerID == 0)
        {
            playerID = GameManager.singleton.localPlayerID;
        }
    }

    void PlayShotSound()
    {

        switch (effectType)
        {
            case MyTimeState.Pause:
                SoundPlayer.GetShot(1);
                break;
            case MyTimeState.SpeedUp:
                SoundPlayer.GetShot(2);
                break;
            case MyTimeState.SlowDown:
                SoundPlayer.GetShot(3);
                break;
            case MyTimeState.BackWard:
                SoundPlayer.GetShot(4);
                break;
            default:
                break;
        }
        


    }
    public void OnTriggerEnter(Collider other)
    {
        if (!isBong) {
            if (other.tag == "Player" && canTrigger == true && other.gameObject != GameManager.singleton.players[playerID - 1])
            {
                MyTestAI ai = other.GetComponent<MyTestAI>();
                if (ai != null) {
                    PlayShotSound();
                    Instantiate(bubbleVFX, transform.position, transform.rotation);
                    canTrigger = false;
                    other.GetComponent<MyTestAI>().SetState(effectType, effectTimeLength);
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (other.tag == "Magic" && canTrigger == true)
            {
                
                TimeArea timeArea = other.GetComponent<TimeArea>();
                if (timeArea != null)
                {
                    SoundPlayer.GetShot(5);
                    //canTrigger = false;
                    timeArea.Reset();
                    if (isDestroyAfterTrigger)
                    {
                        TimeArea selfTimeArea = GetComponent<TimeArea>();
                        selfTimeArea.Reset();
                    }
                    else
                    {
                        Instantiate(bubbleVFX, transform.position, transform.rotation);
                    }
                }
            }
        }
    }
}
