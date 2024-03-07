using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] protected BossBulletDataSO data;

    private float _currentDamage = 0;

    public void Attack(float bossDamage)
    {
        _currentDamage = bossDamage + data.Damage;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(var tag in data.HitAbleTag)
        {
            if(collision.CompareTag(tag))
            {
                if (collision.TryGetComponent<IHitAble>(out var hitAble))
                {
                    hitAble.Hit(_currentDamage);
                    Debug.Log($"{_currentDamage} ������ ����");
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
