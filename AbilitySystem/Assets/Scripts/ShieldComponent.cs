using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldComponent : MonoBehaviour
{
    public float Shield { get; private set; } = 0;
    public void SetShield(float shield) { Shield = shield; }
    public static float TotalShield;

    public void Init(float shield, float time)
    {
        Shield = shield;
        if (time == -1f)
        {
            Shield = shield;
        }
        else
        {
            StartCoroutine(TimedShield(shield, time));
        }
    }

    IEnumerator TimedShield(float shield, float time)
    {
        Shield = shield;
        TotalShield += Shield;
        yield return new WaitForSeconds(time);
        TotalShield -= shield;
        Destroy(this);
    }
}
