using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerRootState
{
    ParticleSystem moveParticle;
    GameObject _visual;

    bool increase = false;
    float speedFactor = 0;

    public PlayerMoveState(PlayerController controller) : base(controller)
    {

        moveParticle = GameObject.Find("MoveParticle").GetComponent<ParticleSystem>();
        _visual = GameObject.Find("PlayerVisual");

    }


    protected override void EnterState()
    {

        speedFactor = SynergyManager.Instance.GetStatFactor(TriggerID.Move);
        moveParticle.Play();
        _visual.transform.DOScale(new Vector2(0.8f, 1.3f), 0.2f).SetEase(Ease.InOutBack);

    }

    protected override void UpdateState()
    {

        //rigid.transform.up = inputController.MoveDir;

        float rotateDegree = Mathf.Atan2(inputController.MoveDir.y, inputController.MoveDir.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotateDegree - 90);
        _visual.transform.rotation = Quaternion.Lerp(_visual.transform.rotation, targetRotation, Time.deltaTime * 15);

        if (increase)
        {

            _visual.transform.localScale = new Vector3(_visual.transform.localScale.x, _visual.transform.localScale.y + Time.deltaTime * 3, _visual.transform.localScale.z);

        }
        else
        {

            _visual.transform.localScale = new Vector3(_visual.transform.localScale.x, _visual.transform.localScale.y - Time.deltaTime * 3, _visual.transform.localScale.z);

        }

        if (_visual.transform.localScale.y < 1.2f)
        {

            increase = true;

        }
        if (_visual.transform.localScale.y > 1.4f)
        {

            increase = false;

        }

        rigid.velocity = _visual.transform.up * playerData.MoveSpeed * (1 + speedFactor);

    }


    protected override void ExitState()
    {

        moveParticle.Stop();
        DOTween.Sequence()
            .Append(_visual.transform.DOScale(new Vector2(1.3f, 0.8f), 0.2f).SetEase(Ease.InOutBack))
            .Append(_visual.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce));

        rigid.velocity = Vector2.zero;

    }



}