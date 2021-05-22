using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeArea : MonoBehaviour
{
    public enum State
    {
        NotInitialize,
        Initialized,
        Set
    }

    public float survivalTime = 3f;
    public float speedFactor = 1f;

    private State state;
    private Vector3 targetPosition;
    private TimeAreaManager manager;
    private float currentTime;

    private void Awake()
    {
        state = State.NotInitialize;
    }

    private void Start()
    {
        manager = transform.parent.GetComponent<TimeAreaManager>();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Initialized:
                if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
                {
                    Vector3 direction = (targetPosition - transform.position).normalized;
                    transform.position += speedFactor * direction * Time.fixedDeltaTime;
                }
                else
                {
                    state = State.Set;
                }
                break;
            case State.Set:
                currentTime -= Time.fixedDeltaTime;
                if (currentTime < 0)
                    manager.DisableTimeArea(this);
                break;
        }
    }

    public void Initialize(Vector3 target)
    {
        target.y = transform.position.y;
        targetPosition = target;
        currentTime = survivalTime;
        state = State.Initialized;
    }
}