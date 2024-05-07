using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FIdleState : BossBaseState
{
    private FlowerBoss _boss;

    public FIdleState(FlowerBoss boss, FlowerPattern pattern) : base(boss, pattern)
    {
        _boss = boss;
    }

    public override void OnBossStateExit()
    {
        _boss.SetBody(_boss.bigestBody, Vector3.one, Vector3.zero, _boss.bossColor, 0.5f);
        _boss.SetBody(_boss.mediumSizeBody, Vector3.one, Vector3.zero, _boss.bossColor, 0.5f);
        _boss.SetBody(_boss.smallestBody, Vector3.one, Vector3.zero, _boss.bossColor, 0.5f);
    }

    public override void OnBossStateOn()
    {
        _boss.gameObject.layer = LayerMask.NameToLayer("Default");
        _boss.isIdle = true;
        _boss.StartCoroutine(IdleAnimation(0.5f));
    }

    public override void OnBossStateUpdate()
    {
        
    }

    // hitShakeFeedback���� sprite�� �ʱ� ���� ��� ���µ� ���⼭ ó�� BigestBody���� ������� �ٲ㼭 hitShakeFeedback�� BigestBody���� ������� ������ �ع���
    private IEnumerator IdleAnimation(float animTime)
    {
        yield return new WaitForSeconds(animTime / 2);
        _boss.StartCoroutine(_boss.Blinking(_boss.bigestBody, animTime, 1, 1, Color.white));
        yield return new WaitForSeconds(animTime / 2);
        _boss.StartCoroutine(_boss.Blinking(_boss.mediumSizeBody, animTime, 1, 1, Color.white));
        yield return new WaitForSeconds(animTime / 2);
        _boss.StartCoroutine(_boss.Blinking(_boss.smallestBody, animTime, 1, 1, Color.white));
        yield return new WaitForSeconds(animTime);

        _boss.isIdle = false;
    }
}
