using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireball")]
public class FireballAbility : Ability
{
    GameObject fireball;
    public float speed;

    public override void Init()
    {

    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveAbilitiy();
        fireball = Instantiate(prefab, user.transform.position, Quaternion.identity);
        fireball.GetComponent<FireballPrefab>().Init(user, target, damage, speed, duration);
    }

    public override void Unload()
    {
        if (fireball != null)
        {
            Destroy(fireball);
        }
    }

}