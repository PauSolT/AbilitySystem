using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Element/Water")]
public class WaterElement : Element
{

    [SerializeField] float empoweredCooldown = 20f;
    [NonSerialized] bool empowered = true;

    public override void Init()
    {

    }
    public override void PassiveOnSwitch()
    {

    }

    public override void PassiveOffSwitch()
    {

    }

    public override void PassiveOnAbilitiy()
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
