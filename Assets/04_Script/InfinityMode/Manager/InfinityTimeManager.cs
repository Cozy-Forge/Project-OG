using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityTimeManager : MonoBehaviour
{
    public static InfinityTimeManager Instance;

    private float waveTime = 30f;
    private float curTime = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"{transform} : InfinityTimeManager is multiply running!");
            Destroy(gameObject);
        }
    }
}
