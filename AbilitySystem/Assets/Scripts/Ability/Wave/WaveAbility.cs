using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Wave")]
public class WaveAbility : Ability
{
    GameObject wave;
    public float speedMax;
    public float speedMin;
    public float timeToChange;
    public float slowDelay;
    public override void AbilityUse(GameObject user, GameObject target)
    {
        wave = Instantiate(prefab, user.transform.position, Quaternion.identity);
        wave.GetComponent<WavePrefab>().Init(user, target, damage, speedMax, speedMin, timeToChange, slowDelay, duration);
    }

    public override void Unload()
    {
        if (wave != null)
        {
            Destroy(wave);
        }
    }

}
