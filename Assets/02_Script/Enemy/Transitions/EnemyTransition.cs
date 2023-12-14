using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;
using System.Diagnostics;

public enum TransitionPlayerCheck
{

    Greater,
    //�÷��̾ ������ �Դ�
    Less
    //�÷��̾ �־�����
    
}


public class EnemyTransition : FSM_Transition<UngonState>
{

    private TransitionPlayerCheck checkType;
    private float checkValue;

    public EnemyTransition(FSM_Controller<UngonState> controller, UngonState nextState, float value, TransitionPlayerCheck checkType) : base(controller, nextState)
    {
        checkValue = value;
        this.checkType = checkType;
    }

    protected override bool CheckTransition()
    {
        switch (checkType)
        {
            case TransitionPlayerCheck.Greater:
                return UngonController.Range > checkValue;
            case TransitionPlayerCheck.Less:
                return UngonController.Range <= checkValue;
            default:
                return false;
        }
    }
}
