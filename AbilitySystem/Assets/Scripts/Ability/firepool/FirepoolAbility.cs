using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Firepool")]
public class FirepoolAbility : Ability
{
    GameObject firepool;
    public float interval;
    public override IEnumerator AbilityUse(GameObject user, GameObject target)
    {
        yield return new WaitForSeconds(castTime);
        firepool = Instantiate(prefab, target.transform.position, Quaternion.identity);
        firepool.GetComponent<FirepoolPrefab>().Init(user, target, damage, interval);
    }

    public override void Unload()
    {
        if (firepool != null)
        {
            Destroy(firepool);
        }
    }

}
