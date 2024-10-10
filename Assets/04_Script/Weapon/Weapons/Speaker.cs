using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Speaker : InvenWeapon
{

    [SerializeField] AudioClip clip;
    [SerializeField] SpeakerAttack effect;
    private bool isAttack = false;

    public override void Attack(Transform target)
    {
        if (!GameManager.Instance.isGamePlay)
            return;

        if (_attackSoundClip != null)
        {

            SoundManager.Instance.SFXPlay("AttackSound", _attackSoundClip, 0.5f);

        }

        DOTween.Sequence().
            Append(transform.DOScale(Vector2.one * 1.5f, 0.2f).SetEase(Ease.InBounce)).
            Append(transform.DOScale(Vector2.one, 0.2f));

        StartCoroutine(AttackTween());

    }

    private IEnumerator AttackTween()
    {

        isAttack = true;
        yield return new WaitForSeconds(0.2f);
        var obj = Instantiate(effect, transform.position, Quaternion.identity);
        obj.SetDamage(Data.GetDamage());
        yield return new WaitForSeconds(0.2f);
        isAttack = false;

    }

    [BindExecuteType(typeof(SendData))]
    public override void GetSignal([BindParameterType(typeof(SendData))] object signal)
    {
        var data = (SendData)signal;

        SkillManager.Instance.RegisterSkill(data.TriggerID, this, data);

    }

    protected override void RotateWeapon(Transform target)
    {
        if (target == null)
        {
            transform.rotation = Quaternion.identity;
            return;
        }
        if (isAttack == true) return;

        var dir = target.position - transform.position;
        dir.Normalize();
        dir.z = 0;

        transform.right = dir;

    }

}
