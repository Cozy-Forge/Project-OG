using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : InvenWeapon
{
    SpriteRenderer _spriteRenderer;
    private bool isAttack = false;

    [SerializeField] EMPBomb empBomb;

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
    }

    public override void Attack(Transform target)
    {
        isAttack = true;

        Vector3 targetPos = target.position;
        Instantiate(empBomb, transform.position, transform.rotation)
            .Throw(targetPos, damage: Data.AttackDamage.GetValue());

        StartCoroutine(WaitAttackEnd());
        
    }

    private IEnumerator WaitAttackEnd()
    {
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }

    [BindExecuteType(typeof(float))]
    public override void GetSignal([BindParameterType(typeof(float))] object signal)
    {

        var data = (SendData)signal;

        if (!sendDataList.ContainsKey(data.GetHashCode()))
        {
            sendDataList.Add(data.GetHashCode(), data);
        }
        else
        {
            sendDataList[data.GetHashCode()].Power = sendDataList[data.GetHashCode()].Power > data.Power ? sendDataList[data.GetHashCode()].Power : data.Power;
        }

    }

    protected override void RotateWeapon(Transform target)
    {
        if (target == null) return;
        if (isAttack == true) return;

        var dir = target.position - transform.position;
        dir.Normalize();
        dir.z = 0;

        _spriteRenderer.flipY = dir.x switch
        {

            var x when x > 0 => false,
            var x when x < 0 => true,
            _ => _spriteRenderer.flipY

        };

        transform.right = dir;

    }

    public override void Run(Transform target)
    {
        base.Run(target);

        if (!isAttack)
        {

            transform.localPosition = origin;

        }

    }

    public override void OnRePosition()
    {
        origin = transform.localPosition;
    }

}
