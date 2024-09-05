using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash")]
public class DashAbility : Ability
{
    Rigidbody2D rb;
    PlayerMovement playerMovement;
    public float timeForSecondActivation;

    Coroutine timer;

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
        float gravityScale = rb.gravityScale;
        float normalRunSpeed = playerMovement.GetRunSpeed();
        rb.gravityScale = 0;
        playerMovement.Dashing = true;
        if (currentCharges == 2)
        {
            timer = GlobalCoroutines.Instance.StartCoroutine(StartTimerForSecondActivation());
        }
        else if (currentCharges == 1)
        {
            GlobalCoroutines.Instance.StopCoroutine(timer);
        }
        yield return new WaitForSeconds(duration);

        rb.gravityScale = gravityScale;
        playerMovement.SetRunSpeed(normalRunSpeed);
        playerMovement.Dashing = false;

    }

    IEnumerator StartTimerForSecondActivation()
    {
        yield return new WaitForSeconds(timeForSecondActivation);
        StartCooldown();
    }

}
