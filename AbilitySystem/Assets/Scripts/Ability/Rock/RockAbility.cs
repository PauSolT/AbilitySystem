using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Rock")]
public class RockAbility : Ability
{
    GameObject rock;
    public float speed;

    public override void Init()
    {

    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveOnAbilitiy();
        Vector2 direction = target - user.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rock = Instantiate(prefab, user.transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
        rock.GetComponent<RockPrefab>().Init(user, target, damage, speed, duration, element);
    }

    public override void Unload()
    {
        if (rock != null)
        {
            Destroy(rock);
        }
    }

}