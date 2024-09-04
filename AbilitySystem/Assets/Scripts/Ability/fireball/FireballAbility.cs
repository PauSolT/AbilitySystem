using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Fireball")]
public class FireballAbility : Ability
{
    GameObject fireball;
    public float speed;

    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveOnAbilitiy();
        fireball = Instantiate(prefab, user.transform.position, Quaternion.identity);
        fireball.GetComponent<FireballPrefab>().Init(user, target, damage, speed, duration, element);
    }

    public override void Unload()
    {
        if (fireball != null)
        {
            Destroy(fireball);
        }
    }

}