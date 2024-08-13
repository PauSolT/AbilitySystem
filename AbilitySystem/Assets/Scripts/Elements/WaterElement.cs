using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Element/Water")]
public class WaterElement : Element
{

    [SerializeField] float empoweredCooldown = 20f;
    [SerializeField] bool empowered = true;

    public override void Init()
    {

    }
    public override void PassiveSwitch()
    {

    }

    public override void PassiveAbilitiy()
    {

    }

    public bool HasEmpoweredAbility()
    {
        return empowered;
    }

    public IEnumerator UsedEmpowered()
    {
        empowered = false;
        yield return new WaitForSeconds(empoweredCooldown);
        empowered = true;
    }
}
