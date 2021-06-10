using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class MyGetTimescale : MonoBehaviour
{
    public Clock refClock;
    private LocalClock localClock;
    // Start is called before the first frame update
    void Start()
    {
        localClock = GetComponent<LocalClock>();
    }

    // Update is called once per frame
    void Update()
    {
        if (localClock != null)
        {
            localClock.localTimeScale = refClock.localTimeScale;
            localClock.paused = refClock.paused;
        }
    }
}
