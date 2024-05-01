using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeModeObj : MonoBehaviour
{
    bool isOn = false;
    Coroutine co = null;
    float coolDownVal = 20f;
    int lastPower = 0;

    public void Excute(Weapon weapon, int power)
    {
        if (power != lastPower)
        {
            weapon.Data.CoolDown -= coolDownVal * lastPower;
            lastPower = power;
            weapon.Data.CoolDown += coolDownVal * lastPower;
        }

        isOn = true;
        if (co == null)
            co = StartCoroutine(ISiegeModeOn(weapon));
    }

    IEnumerator ISiegeModeOn(Weapon weapon)
    {
        weapon.Data.CoolDown += coolDownVal * lastPower;
        while (isOn)
        {
            isOn = false;
            yield return null;
        }
        co = null;
        weapon.Data.CoolDown -= coolDownVal * lastPower;
        Debug.Log(weapon.Data.CoolDown);
    }
}
