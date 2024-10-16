using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Boss/BossData")]
public class BossDataSO : ScriptableObject
{
    public float Speed;
    public float StopRadius;
    public float WallCheckRadius;
    public float MoveX;
    public float MoveY;
    public float StopTime;
    public float PatternChangeTime;
    public float MaxHP;
    public float Damage;
}
