using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScaleSpring : MonoBehaviour
{
    public struct Vector3Spring
    {
        public static readonly float Epsilon = 1.0e-6f;
        public static readonly float TwoPi = 2.0f * Mathf.PI;

        public static readonly int Stride = 8 * sizeof(float);

        public Vector3 Value;
        private float m_padding0;
        public Vector3 Velocity;
        private float m_padding1;

        public void Reset()
        {
            Value = Vector3.zero;
            Velocity = Vector3.zero;
        }

        public void Reset(Vector3 initValue)
        {
            Value = initValue;
            Velocity = Vector3.zero;
        }

        public void Reset(Vector3 initValue, Vector3 initVelocity)
        {
            Value = initValue;
            Velocity = initVelocity;
        }

        public Vector3 TrackDampingRatio(Vector3 targetValue, float angularFrequency, float dampingRatio, float deltaTime)
        {
            if (angularFrequency < Epsilon)
            {
                Velocity = Vector3.zero;
                return Value;
            }

            Vector3 delta = targetValue - Value;

            float f = 1.0f + 2.0f * deltaTime * dampingRatio * angularFrequency;
            float oo = angularFrequency * angularFrequency;
            float hoo = deltaTime * oo;
            float hhoo = deltaTime * hoo;
            float detInv = 1.0f / (f + hhoo);
            Vector3 detX = f * Value + deltaTime * Velocity + hhoo * targetValue;
            Vector3 detV = Velocity + hoo * delta;

            Velocity = detV * detInv;
            Value = detX * detInv;

            if (Velocity.magnitude < Epsilon && delta.magnitude < Epsilon)
            {
                Velocity = Vector3.zero;
                Value = targetValue;
            }

            return Value;
        }

        public Vector3 TrackHalfLife(Vector3 targetValue, float frequencyHz, float halfLife, float deltaTime)
        {
            if (halfLife < Epsilon)
            {
                Velocity = Vector3.zero;
                Value = targetValue;
                return Value;
            }

            float angularFrequency = frequencyHz * TwoPi;
            float dampingRatio = 0.6931472f / (angularFrequency * halfLife);
            return TrackDampingRatio(targetValue, angularFrequency, dampingRatio, deltaTime);
        }

        public Vector3 TrackExponential(Vector3 targetValue, float halfLife, float deltaTime)
        {
            if (halfLife < Epsilon)
            {
                Velocity = Vector3.zero;
                Value = targetValue;
                return Value;
            }

            float angularFrequency = 0.6931472f / halfLife;
            float dampingRatio = 1.0f;
            return TrackDampingRatio(targetValue, angularFrequency, dampingRatio, deltaTime);
        }
    }


    //private static readonly float kInterval = 2.0f;
   //private static readonly float kSmallScale = 0.6f;
    //private static readonly float kLargeScale = 2.0f;
    //private static readonly float kMoveDistance = 30.0f;

    private Vector3Spring m_spring;
    private float m_targetScale = 0.0f;
    private float m_lastTickTime = 0.0f;

    public float interval = 0.5f;
    public float smallScale = 0.6f;
    public float largeScale = 2.0f;
    public float frequenceHZ = 6.0f;
    public float halfLife = 0.05f;

    public void Tick(bool isLarge)
    {
        //m_targetScale = kLargeScale;
        m_targetScale = isLarge ? largeScale : smallScale;
        //m_targetScale = (m_targetScale == kSmallScale) ? kLargeScale : kSmallScale;
        m_lastTickTime = Time.time;

       //var effector = GetComponent<BoingEffector>();
       //effector.MoveDistance = kMoveDistance * ((m_targetScale == kSmallScale) ? -1.0f : 1.0f);
    }

    public void Start()
    {
        //Tick(false);
       // m_spring.Reset(m_targetScale * Vector3.one);
        m_spring.Reset(smallScale * Vector3.one);
    }

    public void FixedUpdate()
    {
        if (Time.time - m_lastTickTime > interval)
            Tick(true);

        m_spring.TrackHalfLife(m_targetScale * Vector3.one, frequenceHZ, halfLife, Time.fixedDeltaTime);
        transform.localScale = m_spring.Value;

        //var effector = GetComponent<BoingEffector>();
        //effector.MoveDistance *= Mathf.Min(0.99f, 35.0f * Time.fixedDeltaTime);
    }

   

    public void ResetSize()
    {
        m_spring.Reset(smallScale * Vector3.one);
        transform.localScale = smallScale *Vector3.one;
    }
}
