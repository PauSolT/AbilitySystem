using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(HealthComponent))]
public class ShieldComponent : MonoBehaviour
{
    public static event Action OnShieldDestroy;
    public float Shield { get; private set; } = 0;
    public HealthComponent totalShield;

    public void Init(float shield, float time)
    {
        totalShield = GetComponent<HealthComponent>();
        CheckIfShieldHasTime(shield, time);
    }

    void CheckIfShieldHasTime(float shield, float time)
    {
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
        OnShieldDestroy?.Invoke();
        Destroy(this);
    }

    void CheckToDestroyShield()
    {
        if (Shield <= 0)
        {
            OnShieldDestroy?.Invoke();
            Destroy(this);
        }
    }
}
