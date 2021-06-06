using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TimeAreaManager : NetworkBehaviour
{
    public TimeArea timeAreaPrefab;

    private PrefabPool<TimeArea> pool;
    
    private void Start()
    {
        pool = new PrefabPool<TimeArea>(timeAreaPrefab);
    }

    [Command(requiresAuthority = false)]
    public void CreateTimeAreaByServerCalling(Vector3 position, Quaternion rotation, Vector3 target, float velocity)
    {
        CreateTimeArea(position, rotation, target, velocity);
    }

    [ClientRpc]
    public void CreateTimeArea(Vector3 position, Quaternion rotation, Vector3 target, float velocity)
    {
        TimeArea timeArea;
        timeArea = pool.Rent();
        timeArea.transform.position = new Vector3(position.x, 1f, position.z);
        timeArea.transform.rotation = rotation;
        timeArea.transform.parent = transform;
        timeArea.Initialize(target, velocity);
    }

    public void DisableTimeArea(TimeArea timeArea)
    {
        pool.Return(timeArea);
    }
}
