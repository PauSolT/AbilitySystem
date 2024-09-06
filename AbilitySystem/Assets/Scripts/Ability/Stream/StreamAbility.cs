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
        base.Init();
        waterElement = element as WaterElement;
    }

    public override void AbilityUse(GameObject user, Vector3 target)
    {
        stream = Instantiate(prefab, user.transform.position + new Vector3(prefab.transform.localScale.z / 2 + 2f, 0.5f, 0), Quaternion.identity, user.transform);
        if (waterElement.HasEmpoweredAbility())
        {
            stream.GetComponent<StreamPrefab>().Init(user, damage * damageMultiplier, interval, duration, element);
            GlobalCoroutines.Instance.StartCoroutine(waterElement.UsedEmpowered());
        }
        else
        {
            stream.GetComponent<StreamPrefab>().Init(user, damage, interval, duration, element);
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