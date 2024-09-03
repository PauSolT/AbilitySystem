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
            lastUseTime = Time.time;
            if (targetingType == TargetingType.Mouse)
            {
                AbilityUse(user, target);
            }
            else
            {
                AbilityUse(user);
            }

            if (blockFromUsingOtherAbilities)
            {
                GlobalCoroutines.Instance.StartCoroutine(BlockFromUsingAbilities());
            }
        }
    }

    IEnumerator BlockFromUsingAbilities()
    {
        PlayerElements.BlockedFromUsingAbilities = true;
        yield return new WaitForSeconds(duration);
        PlayerElements.BlockedFromUsingAbilities = false;
    }

    public abstract void AbilityUse(GameObject user, Vector3 target = new Vector3());

    public abstract void Unload();

    public abstract void Init();
}

public enum TargetingType
{
    Mouse,
    Self,
    Targetted
}