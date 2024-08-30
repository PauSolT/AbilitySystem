using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Firepool")]
public class FirepoolAbility : Ability
{
    GameObject firepool;
    public float interval;

    public override void Init()
    {
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveAbilitiy();
        firepool = Instantiate(prefab, target, Quaternion.identity);
        firepool.GetComponentInChildren<FirepoolPrefab>().Init(user, target, damage, interval, duration);
    }

    public override void Unload()
    {
        if (firepool != null)
        {
            Destroy(firepool);
        }
    }

}
