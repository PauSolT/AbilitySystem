using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Slash")]
public class SlashAbility : Ability
{
    GameObject slash;
    public float speed;
    public float timeForSecondActivation;

    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        if (currentCharges == 0) return;

        slash = Instantiate(prefab, user.transform.position, Quaternion.Euler(0, 0, currentCharges == 2 ? -40f : 40));
        slash.GetComponent<SlashPrefab>().Init(user, damage, speed, duration, element);

        NextCharge(timeForSecondActivation);
    }

    public override void Unload()
    {
        if (slash != null)
        {
            Destroy(slash);
        }
    }

}