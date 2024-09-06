using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Firepool")]
public class FirepoolAbility : Ability
{
    GameObject firepool;
    public float interval;

    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveOnAbilitiy();
        firepool = Instantiate(prefab, target, Quaternion.identity);
        firepool.GetComponentInChildren<FirepoolPrefab>().Init(user, damage, interval, duration, element);
    }

    public override void Unload()
    {
        if (firepool != null)
        {
            Destroy(firepool);
        }
    }

}
