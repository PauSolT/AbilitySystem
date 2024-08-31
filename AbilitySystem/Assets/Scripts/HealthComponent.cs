using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public float maxHealth = 100f;

    [SerializeField]
    float currentHealth;

    [SerializeField]
    bool invincible = false;

    ShieldComponent shield;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    float ShieldTakesDamage(float damage)
    {
        float damageToHealth = 0;
        TryGetComponent(out shield);

        if (!shield) return damageToHealth;

        if (shield && ShieldComponent.TotalShield > 0)
        {
            shield.SetShield(shield.Shield - damage);
            if (shield.Shield < 0 && ShieldComponent.TotalShield > 0)
            {
                ShieldTakesDamage(damage);
            }
            else if (shield.Shield < 0 && ShieldComponent.TotalShield < 0)
            {
                damageToHealth = Mathf.Abs(shield.Shield);
            }
        }
        else
        {
            damageToHealth = 100000;
        }
        return damageToHealth;
    }


    public void TakeDamage(float damageAmount)
    {
        Debug.Log("damageAmount " + damageAmount);
        if (damageAmount <= 0 || invincible) return;

        float shieldDamage = ShieldTakesDamage(damageAmount);
        if (shieldDamage > 0)
        {
            currentHealth -= shieldDamage;
        }
        else
        {
            currentHealth -= damageAmount;
        }
        Debug.Log("currentHealth " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void TakeDamageOverTime(float damageAmount, float duration)
    {
        StartCoroutine(DamageOverTime(damageAmount, duration));
    }

    private IEnumerator DamageOverTime(float damageAmount, float duration)
    {
        float damagePerSecond = damageAmount / duration;
        while (duration > 0)
        {
            TakeDamage(damagePerSecond * Time.deltaTime);
            duration -= Time.deltaTime;
            yield return null;
        }
    }

    public void Heal(float healAmount)
    {
        if (healAmount <= 0) return;

        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


    public void HealingOverTime(float healAmount, float duration)
    {
        StartCoroutine(HealOverTime(healAmount, duration));
    }

    private IEnumerator HealOverTime(float healAmount, float duration)
    {
        float healingPerSecond = healAmount / duration;
        while (duration > 0)
        {
            TakeDamage(healingPerSecond * Time.deltaTime);
            duration -= Time.deltaTime;
            yield return null;
        }
    }

    public void SetInvincible(bool invincible)
    {
        this.invincible = invincible;
    }

    public void SetInvincible(float duration)
    {
        StartCoroutine(InvinciblePeriod(duration));
    }
    public IEnumerator InvinciblePeriod(float duration)
    {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }

    void Die()
    {

    }
}
