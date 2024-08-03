using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireball")]
public class FireballAbility : Ability
{
    GameObject fireball;
    public float speed;
    public override IEnumerator AbilityUse(GameObject user, GameObject target)
    {
        yield return new WaitForSeconds(castTime);
        fireball = Instantiate(prefab, user.transform.position, Quaternion.identity);
        fireball.GetComponent<FireballPrefab>().Init(user, target, damage, speed);
    }

    public override void Unload()
    {
        if (fireball != null)
        {
            Destroy(fireball);
        }
    }

}