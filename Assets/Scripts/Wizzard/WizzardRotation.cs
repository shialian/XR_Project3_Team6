using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizzardRotation : MonoBehaviour
{
    public string mouseXName = "Mouse X";
    public float rotationFactor;

    private float mouseX;
    [HideInInspector]
    public float cameraY;

    private void Start()
    {
        cameraY = 90f;
    }

    private void Update()
    {
        mouseX = Input.GetAxis(mouseXName);
        SetRotation();
    }

    private void SetRotation()
    {
        cameraY += mouseX;
        Vector3 newRotation = new Vector3(0f, cameraY, 0f);
        transform.localRotation = Quaternion.Euler(newRotation);
    }
}
