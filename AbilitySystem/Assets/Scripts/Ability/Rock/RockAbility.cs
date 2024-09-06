using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Rock")]
public class RockAbility : Ability
{
    GameObject rock;
    public float speed;

    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        element.PassiveOnAbilitiy();
        rock = Instantiate(prefab, user.transform.position, Quaternion.AngleAxis(GetRockAngle(user, target) - 90, Vector3.forward));
        rock.GetComponent<RockPrefab>().Init(user, target, damage, speed, duration, element);
    }

    float GetRockAngle(GameObject user, Vector3 target)
    {
        Vector2 direction = target - user.transform.position;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public override void Unload()
    {
        if (rock != null)
        {
            Destroy(rock);
        }
    }

}