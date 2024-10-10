using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfinityTimeManager : MonoBehaviour
{
    public static InfinityTimeManager Instance;

    private float waveTime = 45f;
    private float curTime = 0f;

    [SerializeField] Slider timeSlider;
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

    private void Update()
    {
        if (GameManager.Instance.isGamePlay)
        {
            curTime += Time.deltaTime;
            timeSlider.value = curTime / waveTime;
        }

        if( curTime > waveTime )
        {
            curTime = 0f;
            InfinityModeManager.Instance.Spawn();
        }
    }
}
