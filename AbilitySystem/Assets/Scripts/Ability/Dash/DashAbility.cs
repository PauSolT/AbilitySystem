using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash")]
public class DashAbility : Ability
{
    Rigidbody2D rb;
    PlayerMovement playerMovement;
    public float timeForSecondActivation;
    float gravityScale;
    float normalRunSpeed;

    public override void Init()
    {
        base.Init();
    }
    public override void AbilityUse(GameObject user, Vector3 target)
    {
        rb = user.GetComponent<Rigidbody2D>();
        playerMovement = user.GetComponent<PlayerMovement>();
        GlobalCoroutines.Instance.StartCoroutine(Dash());
    }

    public override void Unload()
    {


    }

    IEnumerator Dash()
    {
        gravityScale = rb.gravityScale;
        normalRunSpeed = playerMovement.GetRunSpeed();
        rb.gravityScale = 0;
        playerMovement.Dashing = true;
        NextCharge(timeForSecondActivation);
        yield return new WaitForSeconds(duration);

        rb.gravityScale = gravityScale;
        playerMovement.SetRunSpeed(normalRunSpeed);
        playerMovement.Dashing = false;

    }

}
