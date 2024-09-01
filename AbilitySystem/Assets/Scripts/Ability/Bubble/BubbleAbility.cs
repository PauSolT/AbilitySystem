using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Bubble")]
public class BubbleAbility : Ability
{
    GameObject bubble;
    public float speed;
    public float damageMultiplier;
    WaterElement waterElement;

    public override void Init()
    {
        waterElement = element as WaterElement;
    }

    public override void AbilityUse(GameObject user, Vector3 target)
    {
        bubble = Instantiate(prefab, user.transform.position, Quaternion.identity);

        if (waterElement.HasEmpoweredAbility())
        {
            bubble.GetComponent<BubblePrefab>().Init(user, target, damage * damageMultiplier, speed, duration, element);
            GlobalCoroutines.Instance.StartCoroutine(waterElement.UsedEmpowered());
        }
        else
        {
            bubble.GetComponent<BubblePrefab>().Init(user, target, damage, speed, duration, element);
        }
    }

    public override void Unload()
    {
        if (bubble != null)
        {
            Destroy(bubble);
        }
    }

}