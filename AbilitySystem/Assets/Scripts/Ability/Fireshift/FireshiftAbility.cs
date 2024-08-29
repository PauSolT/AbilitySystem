using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireshift")]
public class FireshiftAbility : Ability
{

    public override void Init()
    {
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveAbilitiy();
        HealthComponent userHealth = user.GetComponent<HealthComponent>();
        userHealth.SetInvincible(duration);
    }

    public override void Unload()
    {

    }

}
