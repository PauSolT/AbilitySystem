using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class ShieldComponent : MonoBehaviour
{
    public float Shield { get; private set; } = 0;
    public HealthComponent totalShield;

    public void Init(float shield, float time)
    {
        totalShield = GetComponent<HealthComponent>();
        Shield = shield;
        if (time == -1f)
        {
            Shield = shield;
            totalShield.Shield += Shield;
        }
        else
        {
            StartCoroutine(TimedShield(shield, time));
        }
    }
    public void DamageShieldOnly(float damage)
    {
        Shield -= damage;
        CheckToDestroyShield();
    }

    public void ShieldDamage(float shieldDamage)
    {
        Shield -= shieldDamage;
        totalShield.Shield -= shieldDamage;
        CheckToDestroyShield();
    }
    IEnumerator TimedShield(float shield, float time)
    {
        Shield = shield;
        totalShield.Shield += Shield;
        yield return new WaitForSeconds(time);
        totalShield.Shield -= Shield;
        Destroy(this);
    }

    void CheckToDestroyShield()
    {
        if (Shield <= 0)
        {
            Destroy(this);
        }
    }
}
