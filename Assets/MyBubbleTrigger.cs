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

    public void Start()
    {
        canTrigger = true;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (!isBong) {
            if (other.tag == "Player" && canTrigger == true)
            {
                MyTestAI ai = other.GetComponent<MyTestAI>();
                if (ai != null) {
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
