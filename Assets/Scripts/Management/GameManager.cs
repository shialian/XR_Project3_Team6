using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton = null;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(singleton == null)
        {
            singleton = this;
        }
    }

    public void LoadScene(string sceneName)
    {
        ConnectionManager.singleton.ServerChangeScene(sceneName);
    }
}
