using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Wave")]
public class BubbleShiledAbility : Ability
{
    GameObject bubbleShiled;
    WaterElement waterElement;
    public float shield;
    public float shieldMultiplier;

    public override void Init()
    {
        waterElement = element as WaterElement;
    }

    public override void AbilityUse(GameObject user, Vector3 target)
    {
        bubbleShiled = Instantiate(prefab, user.transform.position, Quaternion.identity, user.transform);
        bubbleShiled.GetComponent<BubbleShiledPrefab>().Init(damage, duration);
        if (waterElement.HasEmpoweredAbility())
        {
            user.AddComponent<ShieldComponent>().Init(shield * shieldMultiplier, duration);
            GlobalCoroutines.Instance.StartCoroutine(waterElement.UsedEmpowered());
        }
        else
        {
            user.AddComponent<ShieldComponent>().Init(shield, duration);
        }
    }

    public override void Unload()
    {
        if (bubbleShiled != null)
        {
            Destroy(bubbleShiled);
        }
    }



}
