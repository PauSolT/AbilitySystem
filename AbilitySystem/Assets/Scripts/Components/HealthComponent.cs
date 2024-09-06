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

    public float Shield { get; set; } = 0;
    public float DamageReduction { get; set; } = 0;
    public float HealingPower { get; set; } = 0;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    float ShieldTakesDamage(float damage)
    {
        float damageToHealth = 0;
        //if no damage, return 0
        if (damage <= 0) return damageToHealth;
        //if no shield, return damage
        if (Shield <= 0) return damage;

        shields = GetComponents<ShieldComponent>();

        //if no shield component, return damage
        if (!shields[0] || shields == null) return damage;


        if (shields[0] && Shield > 0)
        {
            shields[0].ShieldDamage(damage);
            //if shield has been destroyed but damage > 0
            //go through other shields and deal damage to them
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
            //if all shields are destroyed but damage > 0
            //return the negative shield of last shield as damage
            //and set shield to 0
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

        damageAmount *= (100f - DamageReduction) / 100f;
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

        currentHealth += healAmount * (1 + (HealingPower / 100f));
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
        Debug.Log(gameObject.name + " died!");
    }
}
