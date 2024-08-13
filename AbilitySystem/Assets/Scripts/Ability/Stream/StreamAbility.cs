using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Stream")]
public class StreamAbility : Ability
{
    GameObject stream;
    public float interval;
    public float damageMultiplier;
    WaterElement waterElement;

    public override void Init()
    {
        waterElement = element as WaterElement;
    }

    public override void AbilityUse(GameObject user, GameObject target)
    {
        stream = Instantiate(prefab, user.transform.position + new Vector3(0, 0.5f, prefab.transform.localScale.z / 2 + 1f), Quaternion.identity, user.transform);
        if (waterElement.HasEmpoweredAbility())
        {
            stream.GetComponent<StreamPrefab>().Init(user, damage * damageMultiplier, interval, duration);
            GlobalCoroutines.Instance.StartCoroutine(waterElement.UsedEmpowered());
        }
        else
        {
            stream.GetComponent<StreamPrefab>().Init(user, damage, interval, duration);
        }
    }

    public override void Unload()
    {
        if (stream != null)
        {
            Destroy(stream);
        }
    }

}