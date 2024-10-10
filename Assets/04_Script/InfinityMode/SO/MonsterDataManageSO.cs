using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/InfinityMode/DataManageSO")]
public class MonsterDataManageSO : ScriptableObject
{
    public List<WaveMonsterDataSO> datas;

    public List<WaveMonsterDataSO> bossDatas;

    public WaveMonsterDataSO RandomPick()
    {
        return datas[Random.Range(0, datas.Count)];
    }
}
