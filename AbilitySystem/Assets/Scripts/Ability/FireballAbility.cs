using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireball")]
public class FireballAbility : Ability
{
    GameObject fireball;
    public override IEnumerator AbilityUse(GameObject user)
    {

        yield return new WaitForSeconds(castTime);
        fireball = Instantiate(prefab, user.transform.position, Quaternion.identity);
        fireball.GetComponent<FireballPrefab>().Init(user, damage);
        Debug.Log("Casting " + abilityName);
    }

    public override void Unload()
    {
        if (fireball != null)
        {
            Destroy(fireball);
            Debug.Log(abilityName + " destroyed!");
        }
    }

}