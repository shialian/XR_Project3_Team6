using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
using Mirror;

public class Whether : NetworkBehaviour
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
    [SyncVar]
    public State state;
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
            if (isServer)
            {
                state = (State)Random.Range(0, 5);
            }
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
                RenderSettings.skybox = skyboxs[0];
                globalClock.localTimeScale = 1f;
                break;
            case State.SpeedUp:
                RenderSettings.skybox = skyboxs[1];
                globalClock.localTimeScale = speedUpFactor;
                break;
            case State.SlowDown:
                RenderSettings.skybox = skyboxs[2];
                globalClock.localTimeScale = slowDownFactor;
                break;
            case State.Pause:
                RenderSettings.skybox = skyboxs[3];
                Invoke("PauseAll", 1.0f);
                Invoke("StartAll", 1.0f + pauseTime);
                break;
            case State.Reverse:
                RenderSettings.skybox = skyboxs[4];
                ReverseAll();
                Invoke("RecoverAll", reverseTime);
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