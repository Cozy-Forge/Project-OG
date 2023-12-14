using DG.Tweening;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public class UngonJumpState : EnemyRootState
{
    public UngonJumpState(UngonController controller) : base(controller)
    {
    }


    protected override void EnterState()
    {
        //점프뛰는거
        Debug.Log("점프");

       

        if (UngonController._targetPos != null)
        {

            var poss = GetAblePos(UngonController._targetPos);

            Vector3 pos;

            if (poss.Count > 0)
            {

                pos = poss[Random.Range(0, poss.Count)];

            }
            else
            {

                pos = transform.position;

            }

            transform.DOJump(pos, enemyData.JumpPower, 1, enemyData.JumpDuration)
                .SetEase(Ease.InSine)
                .OnComplete(() =>
                {

                    enemyData.SetJumpCoolDown();
                    controller.ChangeState(UngonState.Idle);

                });
        }
        else
        {
            enemyData.SetJumpCoolDown();
            controller.ChangeState(UngonState.Idle);

        }

    }

    private List<Vector2> GetAblePos(Transform target)
    {

        var list = new List<Vector2>();

        for (int x = -2; x <= 2; x++)
        {

            for (int y = -2; y <= 2; y++)
            {

                var point = target.position + new Vector3(x, y);

                if (!Physics2D.OverlapCircle(point, 0.5f, LayerMask.GetMask("Wall")))
                {

                    list.Add(point);

                }

            }

        }

        return list;
    }


    protected override void ExitState()
    {
        Debug.Log("나감");
        //rigid.velocity = Vector2.zero;
    }
}
