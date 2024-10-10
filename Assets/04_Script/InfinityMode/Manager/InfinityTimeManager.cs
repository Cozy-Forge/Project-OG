using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfinityTimeManager : MonoBehaviour
{
    public static InfinityTimeManager Instance;

    [HideInInspector]public bool bGameStart = false;

    private float waveTime = 25f;
    private float curTime = 0f;

    [SerializeField] Slider timeSlider;
    [SerializeField] TextMeshProUGUI waveTxt;
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

        if(waveTxt == null || timeSlider == null)
        {
            Debug.LogError("UI Null!");
        }
    }

    private void Update()
    {
        if(bGameStart)
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
                waveTxt.text = $"Shape World\n{InfinityModeManager.Instance.Wave % 10}/{10}";
            }
        }
    }
}
