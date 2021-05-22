using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform leftHandController, rightHandController;
    public Transform head;
    public MovingType type;
    public float speedFactor;

    private Vector2 thumbstickInput;
    private float controllerHeightDiff;
    private Vector3 lastLeftControllerPosition, lastRightControllerPosition;
    private Vector3 currLeftControllerPosition, currRightControllerPosition;

    private void Start()
    {
        currLeftControllerPosition = leftHandController.localPosition;
        currRightControllerPosition = rightHandController.localPosition;
    }

    private void Update()
    {
        if(type == MovingType.Thumbstick)
        {
            thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        }
    }

    private void FixedUpdate()
    {
        switch (type)
        {
            case MovingType.Thumbstick:
                transform.position += new Vector3(thumbstickInput.x, 0.0f, thumbstickInput.y) * Time.fixedDeltaTime;
                break;
            case MovingType.ControllerSwinging:
                if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0 && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0)
                {
                    lastLeftControllerPosition = currLeftControllerPosition;
                    lastRightControllerPosition = currRightControllerPosition;
                    currLeftControllerPosition = leftHandController.localPosition;
                    currRightControllerPosition = rightHandController.localPosition;
                    float leftControllerSwing = Vector3.Distance(lastLeftControllerPosition, currLeftControllerPosition);
                    float rightControllerSwing = Vector3.Distance(lastRightControllerPosition, currLeftControllerPosition);
                    Vector3 forward = new Vector3(head.forward.x, 0f, head.forward.z);
                    transform.position += forward * Time.fixedDeltaTime * (speedFactor * (leftControllerSwing + rightControllerSwing));
                }
                
                break;
        }
    }
}

public enum MovingType
{
    Thumbstick,
    ControllerSwinging
}
