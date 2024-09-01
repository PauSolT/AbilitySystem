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

    [SerializeField]
    ShieldComponent[] shields;

    public float Shield { get; set; }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    float ShieldTakesDamage(float damage)
    {
        float damageToHealth = 0;
        if (damage <= 0) return damageToHealth;
        if (Shield <= 0) return damage;

        shields = GetComponents<ShieldComponent>();

        if (!shields[0] || shields == null) damageToHealth = damage;

        if (shields[0] && Shield > 0)
        {
            shields[0].ShieldDamage(damage);

            if (shields[0].Shield <= 0)
            {
                float restOfDamage = Mathf.Abs(shields[0].Shield);
                int shieldNumber = 0;
                while (restOfDamage != 0 && shieldNumber < shields.Length - 1)
                {
                    shieldNumber++;
                    shields[shieldNumber].DamageShieldOnly(restOfDamage);
                    if (shields[shieldNumber].Shield <= 0)
                    {
                        restOfDamage = Mathf.Abs(shields[shieldNumber].Shield);
                    }
                    else
                    {
                        restOfDamage = 0;
                    }
                }

            }
            if (shields[shields.Length - 1].Shield <= 0)
            {
                damageToHealth = Mathf.Abs(shields[shields.Length - 1].Shield);
                Shield = 0;
            }
        }

        return damageToHealth;
    }


    public void TakeDamage(float damageAmount, Element element)
    {
        if (damageAmount <= 0 || invincible) return;

        //Checks if it has shield, if it doesn't, do full damage
        currentHealth -= ShieldTakesDamage(damageAmount);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
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
