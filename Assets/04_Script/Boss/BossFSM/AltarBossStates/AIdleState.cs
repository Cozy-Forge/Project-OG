using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIdleState : BossBaseState
{
    private AltarBoss _boss;

    public AIdleState(AltarBoss boss, AltarPattern pattern) : base(boss, pattern)
    {
        _boss = boss;
    }

    public override void OnBossStateExit()
    {
        
    }

    public override void OnBossStateOn()
    {
        _boss.StartCoroutine(StartAnimation(0.5f));
    }

    public override void OnBossStateUpdate()
    {
        
    }

    private IEnumerator StartAnimation(float animTime)
    {
        _boss.smallestBody.transform.DOMove(_boss.smallestBody.transform.position + new Vector3(0.6f, 0.5f, 0), animTime)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                _boss.smallestBody.transform.DOMove(_boss.smallestBody.transform.position + new Vector3(-1.2f, 0, 0), animTime)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    _boss.smallestBody.transform.DOMove(_boss.smallestBody.transform.position + new Vector3(0.6f, -1.2f, 0), animTime)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        _boss.smallestBody.transform.DOMove(_boss.smallestBody.transform.position + new Vector3(0, 0.7f, 0), animTime)
                        .SetEase(Ease.InOutSine);
                    });
                });
            });

        yield return new WaitForSeconds(animTime * 4);

        _boss.StartCoroutine(_boss.Blinking(_boss.smallestBody, animTime, 1, 0, Color.white));
        yield return new WaitForSeconds(animTime / 3);
        _boss.StartCoroutine(_boss.Blinking(_boss.mediumSizeBody, animTime, 1, 0, Color.white));
        yield return new WaitForSeconds(animTime / 3);
        _boss.StartCoroutine(_boss.Blinking(_boss.bigestBody, animTime, 1, 0, Color.white));

        _boss.isIdleEnded = true;
    }
}
