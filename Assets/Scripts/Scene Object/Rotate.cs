using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public enum Axis
    {
        x,
        y,
        z
    }

    public float rotationSpeed = 1f;
    public Axis axis = Axis.y;

    private void Update()
    {
        switch (axis) {
            case Axis.x:
                transform.Rotate( rotationSpeed * new Vector3(1, 0, 0));
                break;
            case Axis.y:
                transform.Rotate(rotationSpeed * new Vector3(0, 1, 0));
                break;
            case Axis.z:
                transform.Rotate(rotationSpeed * new Vector3(0, 0, 1));
                break;
        }
    }
}
