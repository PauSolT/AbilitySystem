using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Tornado")]
public class TornadoAbility : Ability
{
    GameObject tornado;
    public float speed;
    public float timeForCooldownActivation;
    public float sizeMultiplier;
    public float damageMultiplier;
    public float damageInterval;
    public float speedMultiplierWhenCombined;

    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        if (currentCharges == 0) return;

        tornado = Instantiate(prefab, target, Quaternion.identity);
        tornado.GetComponentInChildren<TornadoPrefab>().Init(user, target, damage, speed, speedMultiplierWhenCombined, sizeMultiplier, damageMultiplier, duration, damageInterval, element);

        NextCharge(timeForCooldownActivation);
    }

    public override void Unload()
    {
        if (tornado != null)
        {
            Destroy(tornado);
        }
    }

}