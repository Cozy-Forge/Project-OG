using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : InvenWeapon
{
    public override void Attack(Transform target)
    {
        // ��ġ ������ && ��ƼŬ ��ȯ�ؼ� �ǰ�ó�� ���ֱ�
    }

    [BindExecuteType(typeof(SendData))]
    public override void GetSignal([BindParameterType(typeof(SendData))] object signal)
    {

        var data = (SendData)signal;
        SkillContainer.Instance.GetSKill((int)id, (int)data.GeneratorID)?.Excute(transform, target, data.Power);

    }




}
