using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;
using Unity.VisualScripting;

public enum UngonState
{
    Idle,
    Jump,
    Fire,
}


public class UngonController : FSM_Controller<UngonState>
{
    [field: SerializeField] public UngonDataSO enemyData { get; protected set; }
    public static Transform _targetPos;
    public static float Range;


    protected override void Awake()
    {
        _targetPos = GameObject.Find("Player").transform;
        var idleState = new EnemyRootState(this);
        var idleToMove = new EnemyTransition(this, UngonState.Jump, enemyData.JumpRange, TransitionPlayerCheck.Less);
        //idle���� jump ���°� ������ ��
        idleState.AddTransition(idleToMove);
        //�װ� AddTransition ����� ��.

        var jumpState = new UngonJumpState(this);
        var jumpToidle = new EnemyTransition(this, UngonState.Idle, enemyData.JumpRange, TransitionPlayerCheck.Greater);
        //Jump���� idle ���°� ������ ��
        jumpState.AddTransition(jumpToidle);
        //�װ� AddTransition ����� ��.

        AddState(idleState, UngonState.Idle);
        AddState(jumpState, UngonState.Jump);

    }
    protected override void Update()
    {
        if (_targetPos != null)
        {
            Range = Vector3.Distance(_targetPos.position, transform.position);
            //Debug.Log(Range);
        }
        base.Update();
    }

    
}

