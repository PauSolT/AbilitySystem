using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireshift")]
public class FireshiftAbility : Ability
{
    public override void AbilityUse(GameObject user, GameObject target)
    {
        HealthComponent userHealth = user.GetComponent<HealthComponent>();
        userHealth.SetInvincible(duration);
    }

    public override void Unload()
    {

    }

}
