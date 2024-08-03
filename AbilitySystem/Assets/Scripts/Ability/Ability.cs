using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;
    public float cooldown;
    public float duration;
    public float damage;
    public float castTime;

    public GameObject prefab;

    public abstract IEnumerator AbilityUse(GameObject user, GameObject target);
    public abstract void Unload();
}
