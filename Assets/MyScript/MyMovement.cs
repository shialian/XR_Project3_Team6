using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
using Mirror;

public class MyMovement : NetworkBehaviour
{
    public Transform leftHandController, rightHandController;
    public Transform head;
    public MyMovingType type;
    public float speedFactor;

    private Timeline timeLine;
    private Animator anim;
    private Vector2 thumbstickInput;
    private Vector2 keyboardInput;
    private float controllerHeightDiff;
    private Vector3 lastLeftControllerPosition, lastRightControllerPosition;
    private Vector3 currLeftControllerPosition, currRightControllerPosition;
    private LocalClock localClock;

    public float velocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        timeLine = GetComponent<Timeline>();
        currLeftControllerPosition = leftHandController.localPosition;
        currRightControllerPosition = rightHandController.localPosition;

        localClock = GetComponent<LocalClock>();
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
        if (!isLocalPlayer)
        {
            this.enabled = false;
        }
        switch (type)
        {
            case MyMovingType.Thumbstick:
                anim.SetFloat("Forward", thumbstickInput.y);
                anim.SetFloat("Turn", thumbstickInput.x);
                transform.position += speedFactor * new Vector3(thumbstickInput.x, 0.0f, thumbstickInput.y) * timeLine.fixedDeltaTime;
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
                    float animForward = leftControllerSwing + rightControllerSwing;
                    animForward = Mathf.Clamp01(animForward);
                    anim.SetFloat("Forward", animForward);
                    velocity = (speedFactor * (leftControllerSwing + rightControllerSwing));
                    transform.position += forward * timeLine.fixedDeltaTime * velocity;
                }
                else
                {
                    velocity = 0f;
                }
                break;
            case MyMovingType.Keyboard:
                anim.SetFloat("Forward", keyboardInput.y);
                anim.SetFloat("Turn", keyboardInput.x);
                velocity = speedFactor;
                transform.position += new Vector3(keyboardInput.x, 0.0f, keyboardInput.y) * speedFactor * timeLine.fixedDeltaTime;
                break;
        }
    }

    public void ResetAnimator()
    {
        anim.SetFloat("Forward", 0f);
        anim.SetFloat("Turn", 0f);
    }
}

public enum MyMovingType
{
    Thumbstick,
    ControllerSwinging,
    Keyboard
}

