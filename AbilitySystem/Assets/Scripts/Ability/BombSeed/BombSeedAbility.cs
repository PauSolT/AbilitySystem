using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Bomb seed")]
public class BombSeedAbility : Ability
{
    GameObject bombSeed;
    public float speed;
    public float heal;

    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        bombSeed = Instantiate(prefab, user.transform.position, Quaternion.identity);
        bombSeed.GetComponent<BombSeedPrefab>().Init(user, target, damage, heal, speed, duration, element);
    }

    public override void Unload()
    {
        if (bombSeed != null)
        {
            Destroy(bombSeed);
        }
    }

}