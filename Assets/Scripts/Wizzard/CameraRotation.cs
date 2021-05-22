using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public string mouseXName = "Mouse X";
    public string mouseYName = "Mouse Y";
    public float rotationFactor;
    public float verticalLowerBound = -30f;
    public float verticalUpperBound = 55f;

    private float mouseX;
    private float mouseY;
    private float cameraX;
    private float cameraY;

    private void Start()
    {
        cameraX = 0f;
        cameraY = 0f;
    }

    private void Update()
    {
        mouseX = Input.GetAxis(mouseXName);
        mouseY = Input.GetAxis(mouseYName);
        SetRotation();
    }

    private void SetRotation()
    {
        cameraX -= mouseY;
        cameraY += mouseX;
        cameraX = Mathf.Clamp(cameraX, verticalLowerBound, verticalUpperBound);
        Vector3 newRotation = new Vector3(cameraX, cameraY, 0f);
        transform.localRotation = Quaternion.Euler(newRotation);
    }
}
