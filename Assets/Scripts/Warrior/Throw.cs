using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public enum State
    {
        OnYawSelection,
        OnPitchSelection,
        OnForceSelection,
        OnThrowing,
        Throwed
    }

    public GameObject selectionPointer;
    public Rigidbody wizzard;
    public float yawRotateAngle = 1f;
    public float pitchRotateAngle = -1f;
    public float forceScale = -0.01f;

    public State state;

    private int playerID;

    public void Start()
    {
        state = State.OnYawSelection;
        transform.rotation = Quaternion.identity;
        selectionPointer.SetActive(true);
        playerID = GameManager.singleton.localPlayerID;
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A)){
            switch (state)
            {
                case State.OnYawSelection:
                    state = State.OnPitchSelection;
                    break;
                case State.OnPitchSelection:
                    state = State.OnForceSelection;
                    break;
                case State.OnForceSelection:
                    state = State.OnThrowing;
                    Throwing();
                    break;
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.B))
        {
            switch (state)
            {
                case State.OnPitchSelection:
                    state = State.OnYawSelection;
                    break;
                case State.OnForceSelection:
                    state = State.OnPitchSelection;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (state) {
            case State.OnYawSelection:
                OnYawSelection();
                break;
            case State.OnPitchSelection:
                OnPitchSelection();
                break;
            case State.OnForceSelection:
                OnForceSelection();
                break;
        }
    }

    private void OnYawSelection()
    {
        float yaw = transform.rotation.eulerAngles.y;
        if (yaw > 180f)
            yaw -= 360f;
        if (yaw >= 60f || yaw <= -60f)
        {
            yawRotateAngle *= -1f;
        }
        transform.Rotate(0f, yawRotateAngle, 0f);
    }

    private void OnPitchSelection()
    {
        float pitch = selectionPointer.transform.localRotation.eulerAngles.x;
        if (pitch > 180f)
            pitch -= 360f;
        if (pitch > 0f || pitch < -88f)
        {
            pitchRotateAngle *= -1f;
        }
        selectionPointer.transform.Rotate(pitchRotateAngle, 0f, 0f);
    }

    private void OnForceSelection()
    {
        float zSize = selectionPointer.transform.localScale.z;
        zSize += forceScale;
        if (zSize <= 0.01f || zSize > 1f)
            forceScale *= -1f;
        selectionPointer.transform.localScale = new Vector3(1f, 1f, zSize);
    }

    private void Throwing()
    {
        wizzard.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        wizzard.AddForce(selectionPointer.transform.localScale.z * selectionPointer.transform.forward * 2000f);
        wizzard.useGravity = true;
        state = State.Throwed;
        //GameManager.singleton.HasThrowed(playerID);
        this.enabled = false;
    }

    public void ResetAll()
    {
        selectionPointer.transform.rotation = Quaternion.identity;
        selectionPointer.transform.localScale = Vector3.one;
        selectionPointer.SetActive(false);
    }
}