using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Chronos;
using EPOOutline;

public class MyTestAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    private Timeline timeline;
    private LocalClock localClock;
    private Outlinable outlinable;

    public Color[] color;
    public MyTimeState timeState = MyTimeState.Normal;
    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (timeline == null)
        {
            timeline = GetComponent<Timeline>();
        }

        if(localClock == null)
        {
            localClock = GetComponent<LocalClock>();
        }

        if(outlinable == null)
        {
            outlinable = GetComponent<Outlinable>();
        }

        outlinable.OutlineParameters.Enabled = false;

        //timeline.Schedule(
        //        3,
        //        true,
        //        () => { Debug.Log("End Schedule : time = 3 pass"); },
        //        () => { Debug.Log("End Schedule : time = 3 reversed"); }
        //);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }


        switch (timeState)
        {
            case MyTimeState.Normal:
                localClock.paused = false;
                localClock.localTimeScale = 1.0f;
                break;
            case MyTimeState.Pause:
                outlinable.OutlineParameters.Color = color[0];
                localClock.paused = true;
                break;
            case MyTimeState.SpeedUp:
                outlinable.OutlineParameters.Color = color[1];
                localClock.paused = false;
                localClock.localTimeScale = 2.0f;
                break;
            case MyTimeState.SlowDown:
                outlinable.OutlineParameters.Color = color[2];
                localClock.paused = false;
                localClock.localTimeScale = 0.2f;
                break;
            case MyTimeState.BackWard:
                outlinable.OutlineParameters.Color = color[3];
                localClock.paused = false;
                localClock.localTimeScale = -1.0f;
                break;
        }
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    Debug.Log("Start Plan");
        //    timeline.Plan(
        //        5,
        //        true,
        //        ()=> { Debug.Log("End Plan : 5 seconds pass"); },
        //        () => { Debug.Log("End Plan : 5 seconds reversed"); }  
        //    );
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    Debug.Log("Start Do");
        //    timeline.Do(
        //        true,
        //        () => { Debug.Log("End Do: pass"); },
        //        () => { Debug.Log("End Do: reversed"); }
        //    );
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    Debug.Log("Start Memory");
        //    timeline.Memory(
        //        -2,
        //        true,
        //        () => { Debug.Log("End Memory: pass"); },
        //        () => { Debug.Log("End Memory: reversed"); }
        //    );
        //}
    }

    public void SetState(MyTimeState state, float effectTime)
    {
        outlinable.OutlineParameters.Enabled = true;
        timeState = state;
        StartCoroutine(BecomeNormal(effectTime));
    }

    IEnumerator BecomeNormal(float time)
    {
        yield return new WaitForSeconds(time);
        outlinable.OutlineParameters.Enabled = false;
        timeState = MyTimeState.Normal;
    }
}

public enum MyTimeState
{
    Normal,
    Pause,
    SpeedUp,
    SlowDown,
    BackWard
}
