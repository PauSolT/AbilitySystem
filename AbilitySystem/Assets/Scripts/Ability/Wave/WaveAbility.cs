using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Wave")]
public class WaveAbility : Ability
{
    GameObject wave;
    WaterElement waterElement;
    public float speedMax;
    public float speedMin;
    public float timeToChange;
    public float slowDelay;
    public float damageMultiplier;

    public override void Init()
    {
        waterElement = element as WaterElement;

    }

    public override void AbilityUse(GameObject user, GameObject target)
    {
        wave = Instantiate(prefab, user.transform.position, Quaternion.identity);
        if (waterElement.HasEmpoweredAbility())
        {
            wave.GetComponent<WavePrefab>().Init(user, target, damage * damageMultiplier, speedMax, speedMin, timeToChange, slowDelay, duration);
            GlobalCoroutines.Instance.StartCoroutine(waterElement.UsedEmpowered());
        }
        else
        {
            wave.GetComponent<WavePrefab>().Init(user, target, damage, speedMax, speedMin, timeToChange, slowDelay, duration);
        }
    }

    public override void Unload()
    {
        if (wave != null)
        {
            Destroy(wave);
        }
    }



}
