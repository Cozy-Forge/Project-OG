using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Enemy/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    
    [field: SerializeField] public int MaxHP { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float AttackPower { get; set; }
    [field: SerializeField] public float AttackAbleRange { get; set; }
    [field: SerializeField] public bool CheckObstacle { get; set; }
    [field: SerializeField] public float Range { get; set; }
    [field: SerializeField] public float AttackCoolDown { get; set; }
    [field: SerializeField] public float IdleTime { get; set; }  //PatrolAction 사용하는 애들만 // 도착하고 얼마나 쉴지
    [field: SerializeField] public LayerMask TargetAbleLayer { get; private set; }
    [field: SerializeField] public LayerMask ObstacleLayer { get; private set; }
  //[field: SerializeField] public LayerMask RestrictMovementLayer { get; private set; } //아예 못지나가는 layer만.

    public bool IsAttackCoolDown { get; private set; }

    public void SetCoolDown()
    {
        if (IsAttackCoolDown) return;
        IsAttackCoolDown = true;

        FAED.InvokeDelay(() =>
        {

            IsAttackCoolDown = false;

        }, AttackCoolDown);

    }
}
