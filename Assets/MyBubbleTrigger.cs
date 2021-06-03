using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBubbleTrigger : MonoBehaviour
{
    public bool canTrigger = true;

    public MyTimeState effectType;
    public float effectTimeLength;
    public GameObject bubbleVFX;

    public void Start()
    {
        canTrigger = true;
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "TimeTarget" && canTrigger == true)
        {
            Instantiate(bubbleVFX, transform.position, transform.rotation);
            canTrigger = false;
            other.GetComponent<MyTestAI>().SetState(effectType, effectTimeLength);
            gameObject.SetActive(false);
        }
    }
}
