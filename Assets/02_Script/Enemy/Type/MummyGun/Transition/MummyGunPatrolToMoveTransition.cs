using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyGunPatrolToMoveTransition : BaseFSM_Transition<EMummyGunState>
{
    Transform playerTrm;
    public MummyGunPatrolToMoveTransition(BaseFSM_Controller<EMummyGunState> controller, EMummyGunState nextState) : base(controller, nextState)
    {
        playerTrm = GameManager.Instance.player;
    }

    protected override bool CheckTransition()
    {
        // �Ÿ� �ȿ� �ְ� ���� �Ҽ� �ִ� �����϶�.
        return Transitions.CheckDistance(playerTrm, this.transform, _data.Range) 
            && !_data.IsAttackCoolDown;
    }
}
