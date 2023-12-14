using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public enum TransitionPlayerCheck
{

    Equal,
    //플레이어가 가까이 왔다
    Greater,
    //플레이어가 멀어졌다
    Less

}


public class EnemyTransition : FSM_Transition<UngonState>
{
    public EnemyTransition(FSM_Controller<UngonState> controller, UngonState nextState) : base(controller, nextState)
    {
    }

    protected override bool CheckTransition()
    {
        throw new System.NotImplementedException();
    }
}
