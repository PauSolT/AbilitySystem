using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireshift")]
public class FireshiftAbility : Ability
{
    public override IEnumerator AbilityUse(GameObject user, GameObject target)
    {
        HealthComponent userHealth = user.GetComponent<HealthComponent>();
        yield return new WaitForSeconds(castTime);
        userHealth.SetInvincible(duration);
    }

    public override void Unload()
    {

    }

}
