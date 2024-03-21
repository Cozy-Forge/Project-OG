using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] protected BossBulletDataSO data;

    private float f_currentDamage = 0;

    protected virtual void OnEnable()
    {
        StartCoroutine(ObjectPool.Instance.ReturnObject(this.gameObject, data.DestoryTime));
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Attack(float bossDamage)
    {
        f_currentDamage = bossDamage + data.Damage;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(var tag in data.HitAbleTag)
        {
            if(collision.CompareTag(tag))
            {
                if (collision.TryGetComponent<IHitAble>(out var hitAble))
                {
                    hitAble.Hit(f_currentDamage);
                    Debug.Log($"{f_currentDamage} ������ ����");
                    if (data.IfHitWillBreak)
                        ObjectPool.Instance.ReturnObject(gameObject);
                }
                else
                {
                    //Debug.Log($"�÷��̾�� {_currentDamage}��ŭ �������� ��");
                    if (data.IfHitWillBreak)
                        ObjectPool.Instance.ReturnObject(gameObject);
                }
            }
        }
    }

}
