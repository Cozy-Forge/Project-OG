using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityModeManager : MonoBehaviour
{
    public static InfinityModeManager Instance;

    [SerializeField] MonsterDataManageSO manageData;
    private int wave = 0;
    public int Wave => wave;

    Transform[] spawnPoints;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : InfinityModeManager is multiply running!");
            Destroy(gameObject);
        }

        spawnPoints = GetComponentsInChildren<Transform>();
    }

    public void Spawn() => StartCoroutine(ISpawn());

    IEnumerator ISpawn()
    {
        wave++;

        int spawnCnt = Random.Range(wave,wave * 2) + Random.Range(5,10);
        int test = 0;

        for(int i = 0; i < spawnCnt; i++)
        {
            WaveMonsterDataSO randomData = manageData.RandomPick();
            while(randomData.appearlevel > wave) 
            {
                if(test++ >= 100001)
                {
                    Debug.LogError("infinite loop");
                    break;
                }

                randomData = manageData.RandomPick();
            }
            Instantiate(randomData.monster, spawnPoints[Random.Range(0, spawnPoints.Length)]);
            yield return new WaitForSeconds(Random.Range(0.15f, 0.4f));
        }
    }
}
