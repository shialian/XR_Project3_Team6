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
        switch (type)
        {
            case MovingType.Thumbstick:
                thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                break;
            case MovingType.ControllerSwinging:
                lastLeftControllerPosition = currLeftControllerPosition;
                lastRightControllerPosition = currRightControllerPosition;
                currLeftControllerPosition = leftHandController.localPosition;
                currRightControllerPosition = rightHandController.localPosition;
                controllerHeightDiff = Mathf.Abs(leftHandController.transform.position.y - rightHandController.position.y);
                break;
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
                float leftControllerSwing = Vector3.Distance(lastLeftControllerPosition, currLeftControllerPosition);
                float rightControllerSwing = Vector3.Distance(lastRightControllerPosition, currLeftControllerPosition);
                if (leftControllerSwing > 0.005f && rightControllerSwing > 0.005f)
                {
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
