using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMovement : MonoBehaviour
{
    public Transform leftHandController, rightHandController;
    public Transform head;
    public MyMovingType type;
    public float speedFactor;


    private Vector2 thumbstickInput;
    private Vector2 keyboardInput;
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
        if (type == MyMovingType.Thumbstick)
        {
            thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        }

        if (type == MyMovingType.Keyboard)
        {
            keyboardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    private void FixedUpdate()
    {
        switch (type)
        {
            case MyMovingType.Thumbstick:
                transform.position += new Vector3(thumbstickInput.x, 0.0f, thumbstickInput.y) * Time.fixedDeltaTime;
                break;
            case MyMovingType.ControllerSwinging:
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
            case MyMovingType.Keyboard:
                transform.position += new Vector3(keyboardInput.x, 0.0f, keyboardInput.y) * speedFactor * Time.fixedDeltaTime;
                break;
        }
    }
}

public enum MyMovingType
{
    Thumbstick,
    ControllerSwinging,
    Keyboard
}

