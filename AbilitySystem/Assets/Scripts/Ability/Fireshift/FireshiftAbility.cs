using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireshift")]
public class FireshiftAbility : Ability
{
    HealthComponent userHealth;
    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        if (!userHealth)
        {
            userHealth = user.GetComponent<HealthComponent>();
        }
        userHealth.SetInvincible(duration);
        element.PassiveOnAbilitiy();
    }

    public override void Unload()
    {

    }

}
