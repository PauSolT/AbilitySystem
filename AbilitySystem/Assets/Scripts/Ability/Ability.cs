using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [NonSerialized]
    float lastUseTime;

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


    public GameObject prefab;

    public void UseAbility(GameObject user, GameObject target)
    {
        if (!IsOnCooldown())
        {
            lastUseTime = Time.time;
            AbilityUse(user, target);
        }
    }

    public abstract void AbilityUse(GameObject user, GameObject target);
    public abstract void Unload();

    public abstract void Init();
}
