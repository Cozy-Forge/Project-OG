using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRushManager : MonoBehaviour
{
    static public BossRushManager Instance;

    private RandomStageSystem stageSystem;

    float bossHp = 1500f;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"{transform} : BossRushManager is multiply running!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        stageSystem = FindObjectOfType<RandomStageSystem>();
    }

    public void StageClear()
    {
        AddMoney();
        ExpansionInventory();
    }

    private void AddMoney()
    {
        Money.Instance.EarnGold(300 + stageSystem.step * 50);
    }

    private void ExpansionInventory()
    {
        ExpansionManager.Instance.AddSlotcnt(5 + stageSystem.step * 1);
    }

    public float GetMaxHp()
    {
        return bossHp * (stageSystem.step + 1) * (stageSystem.step + 1);
    }
}
