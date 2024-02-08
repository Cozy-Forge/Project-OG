using FSM_System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� Transition
/// </summary>

public class TransitionIdleOrMove<T> : BaseFSM_Transition<T> where T : Enum
{
    enum CheckType
    {
        Idle,
        Move
    }

    Transform playerTrm;
    EnemyDataSO _data;
    CheckType _myType;

    public TransitionIdleOrMove(BaseFSM_Controller<T> controller, T nextState) : base(controller, nextState)
    {

        if (nextState.ToString() == "Idle")
            _myType = CheckType.Idle;
        else if (nextState.ToString() == "Move")
            _myType = CheckType.Move;
        else
            Debug.LogError("Only (idle, Move) can!");

        
        playerTrm = GameManager.Instance.player;

        _data = controller.EnemyData;
        
        #region enum�̸����� ������ �Ҵ�
        //string className = $"{nextState.GetType().Name.Remove(1,1)}Controller";
        //Debug.Log("ClassName : " + className);
        //Type type = Type.GetType($"{className}, Assembly-CSharp");
        //if (type == null)
        //{
        //    Debug.LogError("class�̸��� enum �̸��� ���ϵ��� ����");
        //}
        //else
        //{
        //    var enemyDataProperty = type.GetProperty("EnemyData");
        //    if(enemyDataProperty != null)
        //        _data = enemyDataProperty.GetValue(controller) as EnemyDataSO;
        //    else
        //        Debug.LogError("EnemyData property not found in the dynamically obtained type");

        //}
        #endregion
    }

    protected override bool CheckTransition()
    {
        Debug.Log($"nextState : {nextState}");
        //_nextState = _nextState;
        if(_myType == CheckType.Idle) // case: idle��.
        {

            if(_data.CheckObstacle) // ��
            {
                //�����Ÿ� �ۿ� �ְų� ��ֹ��� ������ ��ȯ.
                return !Transitions.CheckDistance(playerTrm, this.transform, _data.Range) ||
                    Transitions.CheckObstacleBetweenTarget(playerTrm, this.transform, EObstacleType.Wall);
            }
            else // ����
            {
                //�����Ÿ� �ۿ� ������ ��ȯ.
                return !Transitions.CheckDistance(playerTrm, this.transform, _data.Range);
            }

        }
        else if (_myType == CheckType.Move) // case: move��.
        {
            if (_data.CheckObstacle) // ��
            {
                //�����Ÿ� �ȿ� �ְ� ���̿� ��ֹ��� ������ ��ȯ
                return Transitions.CheckDistance(playerTrm, this.transform, _data.Range) &&
                        !Transitions.CheckObstacleBetweenTarget(playerTrm, this.transform, EObstacleType.Wall);
            }
            else // ����
            {
                //�����Ÿ� �ȿ� ������ ��ȯ
                return Transitions.CheckDistance(playerTrm, this.transform, _data.Range);
            }
        }
        else
        {
            Debug.LogError("enum sequence was wrong. //idle : 0, move : 1");
        }

        return false;
    }
}
