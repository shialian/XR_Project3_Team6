using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBubbleTrigger : MonoBehaviour
{
    public bool canTrigger = true;

    public MyTimeState effectType;
    public float effectTimeLength;

    public void Start()
    {
        canTrigger = true;
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "TimeTarget" && canTrigger == true)
        {
            canTrigger = false;
            other.GetComponent<MyTestAI>().SetState(effectType, effectTimeLength);
            gameObject.SetActive(false);
        }
    }
}
