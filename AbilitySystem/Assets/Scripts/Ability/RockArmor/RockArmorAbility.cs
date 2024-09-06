using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RockArmor")]
public class RockArmorAbility : Ability
{
    public float interval;
    public float damageReduction;
    HealthComponent playerHealth;
    GameObject armor;

    public override void Init()
    {
        base.Init();
        playerHealth = GameObject.Find("Player").GetComponent<HealthComponent>();
    }

    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveOnAbilitiy();
        playerHealth.DamageReduction += damageReduction;
        armor = Instantiate(prefab, user.transform.position, Quaternion.identity, user.transform);
        armor.GetComponent<RockArmorPrefab>().Init(damage, duration, interval, element, playerHealth, damageReduction);
    }

    public override void Unload()
    {
        if (armor != null)
        {
            Destroy(armor);
        }
    }
}