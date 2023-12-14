using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM_System;

public class EnemyRootState : FSM_State<UngonState>
{
    protected UngonDataSO enemyData;
    protected Rigidbody2D rigid;

    public EnemyRootState(UngonController controller) : base(controller)
    {
        enemyData = controller.enemyData;
        rigid = GetComponent<Rigidbody2D>();
    }
}
