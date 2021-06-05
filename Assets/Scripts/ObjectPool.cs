using System;
using System.Collections.Generic;

public class PrefabPool<T> : IDisposable where T : UnityEngine.Component
{
    public PrefabPool(T prefab)
    {
        if (prefab == null) { UnityEngine.Debug.LogError("Error constructing " + this + ", " + prefab + " is null!"); }
        Prefab = prefab;
    }
    private PrefabPool() { }
    
    public T Prefab { get; private set; }
    bool isDisposed = false;
    Queue<T> q;

    /// <summary>
    /// Limit of instance count.
    /// </summary>
    protected int MaxPoolCount => int.MaxValue;

    /// <summary>
    /// Create instance when needed.
    /// </summary>
    protected virtual T CreateInstance(T prefab = null)
    {
        return prefab == null ? UnityEngine.Object.Instantiate(Prefab) : UnityEngine.Object.Instantiate(prefab);
    }

    /// <summary>
    /// Called before rent from pool, useful for set active object(it is default behavior).
    /// </summary>
    protected virtual void OnBeforeRent(T instance)
    {
        instance.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called before return to pool, useful for set inactive object(it is default behavior).
    /// </summary>
    protected virtual void OnBeforeReturn(T instance)
    {
        instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when clear or disposed, useful for destroy instance or other finalize method.
    /// </summary>
    protected virtual void OnClear(T instance)
    {
        if (instance == null) return;

        var go = instance.gameObject;
        if (go == null) return;
        UnityEngine.Object.Destroy(go);
    }

    /// <summary>
    /// Current pooled object count.
    /// </summary>
    public int Count => q == null ? 0 : q.Count;

    /// <summary>
    /// Get instance from pool.
    /// </summary>
    public virtual T Rent(T prefab = null)
    {
        if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
        if (Prefab == null && prefab == null) { return null; }
        if (q == null) q = new Queue<T>();

        var instance = (q.Count > 0)
            ? q.Dequeue() : prefab == null
            ? CreateInstance() : CreateInstance(prefab);

        OnBeforeRent(instance);
        return instance;
    }

    /// <summary>
    /// Return instance to pool.
    /// </summary>
    public void Return(T instance)
    {
        if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
        if (instance == null) throw new ArgumentNullException("instance");

        if (q == null) q = new Queue<T>();

        if ((q.Count + 1) == MaxPoolCount)
        {
            throw new InvalidOperationException("Reached Max PoolSize");
        }

        OnBeforeReturn(instance);
        q.Enqueue(instance);
    }

    /// <summary>
    /// Clear pool.
    /// </summary>
    public void Clear(bool callOnBeforeRent = false)
    {
        if (q == null) return;
        while (q.Count != 0)
        {
            var instance = q.Dequeue();
            if (callOnBeforeRent)
            {
                OnBeforeRent(instance);
            }
            OnClear(instance);
        }
    }

    /// <summary>
    /// Trim pool instances. 
    /// </summary>
    /// <param name="instanceCountRatio">0.0f = clear all ~ 1.0f = live all.</param>
    /// <param name="minSize">Min pool count.</param>
    /// <param name="callOnBeforeRent">If true, call OnBeforeRent before OnClear.</param>
    public void Shrink(float instanceCountRatio, int minSize, bool callOnBeforeRent = false)
    {
        if (q == null) return;

        if (instanceCountRatio <= 0) instanceCountRatio = 0;
        if (instanceCountRatio >= 1.0f) instanceCountRatio = 1.0f;

        var size = (int)(q.Count * instanceCountRatio);
        size = Math.Max(minSize, size);

        while (q.Count > size)
        {
            var instance = q.Dequeue();
            if (callOnBeforeRent)
            {
                OnBeforeRent(instance);
            }
            OnClear(instance);
        }
    }

    public void SetPrefab(T prefab) => Prefab = prefab;

    #region IDisposable Support

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                Clear(false);
            }

            isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    #endregion
}