using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{

    public float maxHealth = 100f;

    [SerializeField]
    float currentHealth;

    [SerializeField]
    bool invincible = false;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        if (damageAmount <= 0 || invincible) return;

        currentHealth -= damageAmount;

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
