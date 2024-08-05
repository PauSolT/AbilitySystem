using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Bubble")]
public class BubbleAbility : Ability
{
    GameObject bubble;
    public float speed;
    public override void AbilityUse(GameObject user, GameObject target)
    {
        bubble = Instantiate(prefab, user.transform.position, Quaternion.identity);
        bubble.GetComponent<BubblePrefab>().Init(user, target, damage, speed, duration);
    }

    public override void Unload()
    {
        if (bubble != null)
        {
            Destroy(bubble);
        }
    }

}