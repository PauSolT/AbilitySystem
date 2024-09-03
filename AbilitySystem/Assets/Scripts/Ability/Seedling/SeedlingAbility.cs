using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Seedling")]
public class SeedlingAbility : Ability
{
    GameObject seedling;
    public float flightDuration;

    public override void Init()
    {

    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveOnAbilitiy();
        seedling = Instantiate(prefab, user.transform.position, Quaternion.identity);
        seedling.GetComponent<SeedlingPrefab>().Init(user, target, damage, duration, flightDuration, element);
    }

    public override void Unload()
    {
        if (seedling != null)
        {
            Destroy(seedling);
        }
    }

}