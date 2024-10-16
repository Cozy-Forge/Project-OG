using FSM_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStateController : BaseFSM_Controller<ENormalEnemyState>
{
    public Transform target;

    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private Transform _weapon;

    protected override void Start()
    {
        base.Start();

        var rootState = new NormalRootState(this);
        rootState
            .AddTransition<ENormalEnemyState>(new RoomOpenTransitions<ENormalEnemyState>(this, ENormalEnemyState.Move));

        var moveState = new NormalChaseState(this);
        var moveToAttack = new MoveToAttackTransition<ENormalEnemyState>(this, ENormalEnemyState.Attack, true);

        moveState
            .AddTransition<ENormalEnemyState>(moveToAttack);

        // ���̷��� ����
        var attackState = new SkeletonAttackState(this, _attackPoint, _weapon);

        AddState(rootState, ENormalEnemyState.Idle);
        AddState(moveState, ENormalEnemyState.Move);
        AddState(attackState, ENormalEnemyState.Attack);
    }
}
