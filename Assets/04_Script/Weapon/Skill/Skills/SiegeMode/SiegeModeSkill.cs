    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SiegeModeSkill : Skill
{
    [SerializeField] SiegeModeObj siegeModeobj;
    [SerializeField] VisualEffect effect;
    VisualEffect v;

    Dictionary<Tuple<Transform, Transform>, SiegeModeObj> _coolDownDic = new();
    public override void Excute(Transform weaponTrm, Transform target, int power, SendData trigger = null)
    {

        Weapon weapon = weaponTrm.GetComponent<Weapon>();
        if (trigger == null || trigger.trigger == null || weaponTrm == null)
            return;
        var tuple = Tuple.Create(weaponTrm, trigger.trigger);
        if (!_coolDownDic.ContainsKey(tuple))
        {
            _coolDownDic.Add(tuple, Instantiate(siegeModeobj));
        }
        _coolDownDic[tuple].Excute(weapon, power);
    }

}
