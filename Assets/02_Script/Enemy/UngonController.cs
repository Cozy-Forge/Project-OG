using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;


public enum UngonState
{
    Idle,
    Jump,
    Fire,
}


public class UngonController : FSM_Controller<UngonState>
{
    [field: SerializeField] public UngonDataSO enemyData { get; protected set; }

    protected override void Awake()
    {
        var idleState = new EnemyRootState(this);
        //idle���� jump ���°� ������ ��

        //�װ� AddTransition ����� ��.

        var jumpState = new UngonJumpState(this);
        //Jump���� idle ���°� ������ ��

        //�װ� AddTransition ����� ��.

        AddState(idleState, UngonState.Idle);
        AddState(jumpState, UngonState.Jump);

    }
}

