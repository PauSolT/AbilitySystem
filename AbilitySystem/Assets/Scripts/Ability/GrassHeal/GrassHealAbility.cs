using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Grass heal")]
public class GrassHealAbility : Ability
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
        userHealth.Heal(damage);
    }

    public override void Unload()
    {

    }

}