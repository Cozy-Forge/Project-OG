using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UngonJumpState : EnemyRootState
{
    public UngonJumpState(UngonController controller) : base(controller)
    {
    }

    protected override void UpdateState()
    {
        //�����ٴ°�
    }

    protected override void ExitState()
    {
        rigid.velocity = Vector2.zero;
    }
}
