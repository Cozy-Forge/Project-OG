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
        //idle에서 jump 가는걸 만들어야 함

        //그걸 AddTransition 해줘야 함.

        var jumpState = new UngonJumpState(this);
        //Jump에서 idle 가는걸 만들어야 함

        //그걸 AddTransition 해줘야 함.

        AddState(idleState, UngonState.Idle);
        AddState(jumpState, UngonState.Jump);

    }
}

