using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBuildTester : MonoBehaviour
{
    public Camera aCamera;
    public SimpleSonarShader_Object sonar;
    public float intensity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = aCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                sonar.StartSonarRing(hit.point, intensity);
                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
