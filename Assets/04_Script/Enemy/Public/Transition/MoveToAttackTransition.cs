using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAttackTransition<T> : BaseFSM_Transition<T> where T : Enum
{
    Transform targetTrm;
    bool checkObstacle;

    public MoveToAttackTransition(BaseFSM_Controller<T> controller, T nextState, bool checkObstacle) : base(controller, nextState)
    {
        targetTrm = controller.Target;
        this.checkObstacle = checkObstacle;
    }

    protected override bool CheckTransition()
    {
        //공격쿨이 다 돌았고, 공격범위 안에 있을때 
        
        bool result = !_data.IsAttackCoolDown &&
                    Transitions.CheckDistance(controller.transform, targetTrm, _data.AttackAbleRange);

        if (checkObstacle)
        {
            //return result && Transitions.CheckObstacleBetweenTarget(controller.transform, targetTrm, _data.ObstacleLayer);
        }

        return result;
    }
}
