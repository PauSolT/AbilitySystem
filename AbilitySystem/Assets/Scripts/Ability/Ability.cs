using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;
    public float cooldown;
    public float duration;
    public float damage;

    [NonSerialized]
    public float lastUseTime;

    public bool IsOnCooldown()
    {
        return Time.time < lastUseTime + cooldown;
    }

    public float GetRemainingCooldown()
    {
        return Mathf.Max(0, lastUseTime + cooldown - Time.time);
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
}
