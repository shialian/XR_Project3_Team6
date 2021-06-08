using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public string mouseYName = "Mouse Y";
    public float rotationFactor;
    public float verticalLowerBound = -30f;
    public float verticalUpperBound = 55f;

    private float mouseY;
    private float cameraX;

    private void Start()
    {
        cameraX = 0f;
    }

    private void Update()
    {
        mouseY = Input.GetAxis(mouseYName);
        SetRotation();
    }

    private void SetRotation()
    {
        cameraX -= mouseY;
        cameraX = Mathf.Clamp(cameraX, verticalLowerBound, verticalUpperBound);
        Vector3 newRotation = new Vector3(cameraX, 0f, 0f);
        transform.localRotation = Quaternion.Euler(newRotation);
    }
}
