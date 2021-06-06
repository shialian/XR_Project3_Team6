using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimeAreaManager : MonoBehaviour
{
    public TimeArea timeAreaPrefab;

    private PrefabPool<TimeArea> pool;

    private void Start()
    {
        pool = new PrefabPool<TimeArea>(timeAreaPrefab);
    }

    public void CreateTimeArea(Vector3 position, Quaternion rotation, Vector3 target)
    {
        TimeArea timeArea;
        timeArea = pool.Rent();
        timeArea.transform.position = new Vector3(position.x, 1f, position.z);
        timeArea.transform.rotation = rotation;
        timeArea.transform.parent = transform;
        //timeArea.Initialize(target);
    }

    public void DisableTimeArea(TimeArea timeArea)
    {
        pool.Return(timeArea);
    }
}
