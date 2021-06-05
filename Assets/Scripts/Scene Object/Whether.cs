using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class Whether : MonoBehaviour
{
    public float whetherChangeTime = 10f;
    public float speedUpFactor = 1.2f;
    public float slowDownFactor = 0.8f;
    public float pauseTime = 2.0f;
    public float reverseTime = 2.0f;
    public Material[] skyboxs;

    public enum State
    {
        Normal,
        SpeedUp,
        SlowDown,
        Pause,
        Reverse
    }
    private State state;
    public float accumulator;
    private AreaClock3D globalClock;

    private void Start()
    {
        accumulator = 0f;
        globalClock = GetComponent<AreaClock3D>();
        state = State.Normal;
        ChangeState(state);
    }

    private void Update()
    {
        accumulator += Time.deltaTime;
        if (accumulator >= whetherChangeTime)
        {
            state = (State)Random.Range(0, 5);
            //state = State.Reverse;
            ChangeState(state);
            accumulator = 0f;
        }
    }

    private void ChangeState(State state)
    {
        switch (state)
        {
            case State.Normal:
                globalClock.localTimeScale = 1f;
                break;
            case State.SpeedUp:
                globalClock.localTimeScale = speedUpFactor;
                break;
            case State.SlowDown:
                globalClock.localTimeScale = slowDownFactor;
                break;
            case State.Pause:
                Invoke("PauseAll", 1.0f);
                Invoke("StartAll", 1.0f + pauseTime);
                break;
            case State.Reverse:
                Invoke("ReverseAll", 1.0f);
                Invoke("RecoverAll", 1.0f + reverseTime);
                break;
        }
    }

    private void PauseAll()
    {
        globalClock.paused = true;
    }

    private void StartAll()
    {
        globalClock.paused = false;
        state = (State)Random.Range(0, 5);
        accumulator = 0f;
    }

    private void ReverseAll()
    {
        globalClock.localTimeScale = -1f;
    }

    public void RecoverAll()
    {
        state = State.Normal;
        ChangeState(state);
        accumulator = 0f;
    }
}