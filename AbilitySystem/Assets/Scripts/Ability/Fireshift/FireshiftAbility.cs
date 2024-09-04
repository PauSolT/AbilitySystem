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
        if (userHealth == null)
        {
            userHealth = user.GetComponent<HealthComponent>();
        }
        element.PassiveOnAbilitiy();
        userHealth.SetInvincible(duration);
    }

    public override void Unload()
    {

    }

}
