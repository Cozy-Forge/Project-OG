using System;
using System.Collections;
using UnityEngine;

public class DoublePistol : Skill
{

    [SerializeField] private Bullet bullet;
    [SerializeField] float damage;

    public override void Excute(Transform weaponTrm, Transform target, int power)
    {

        StartCoroutine(Shooting(weaponTrm, target, power));

    }

    IEnumerator Shooting(Transform weaponTrm, Transform target, int power)
    {

        var a = Instantiate(bullet, weaponTrm.position, weaponTrm.rotation);
        a.Shoot(power * damage);

        yield return new WaitForSeconds(0.1f);

        var b = Instantiate(bullet, weaponTrm.position, weaponTrm.rotation);
        b.Shoot(power * damage * 2);

    }

}
