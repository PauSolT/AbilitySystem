using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public Element element;
    public string abilityName;
    public string abilityDescription;
    public float cooldown;
    public float duration;
    public float damage;
    public bool blockFromUsingOtherAbilities;
    public int charges;
    protected int currentCharges;

    public TargetingType targetingType;

    [NonSerialized]
    float lastUseTime;
    public GameObject prefab;


    public bool IsOnCooldown()
    {
        return Time.time < lastUseTime + cooldown;
    }

    float GetRemainingCooldown()
    {
        return Mathf.Max(0, lastUseTime + cooldown - Time.time);
    }

    public float GetRemainingCooldownNormalized()
    {
        return (cooldown - GetRemainingCooldown()) / cooldown;
    }


    public void ActivateAbility(GameObject user, Vector3 target = new Vector3())
    {
        if (!IsOnCooldown())
        {
            if (targetingType == TargetingType.Mouse)
            {
                AbilityUse(user, target);
            }
            else
            {
                AbilityUse(user);
            }
            currentCharges--;

            if (blockFromUsingOtherAbilities)
            {
                GlobalCoroutines.Instance.StartCoroutine(BlockFromUsingAbilities());
            }
            if (currentCharges <= 0)
            {
                StartCooldown();
            }
        }
    }

    IEnumerator BlockFromUsingAbilities()
    {
        PlayerElements.BlockedFromUsingAbilities = true;
        yield return new WaitForSeconds(duration);
        PlayerElements.BlockedFromUsingAbilities = false;
    }

    protected void StartCooldown()
    {
        lastUseTime = Time.time;
        currentCharges = charges;
    }

    public abstract void AbilityUse(GameObject user, Vector3 target = new Vector3());

    public abstract void Unload();

    public virtual void Init()
    {
        currentCharges = charges;
    }
}

public enum TargetingType
{
    Mouse,
    Self,
    Targetted,
    Direction
}