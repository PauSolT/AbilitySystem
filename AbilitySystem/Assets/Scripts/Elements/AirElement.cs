using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Element/Air")]
public class AirElement : Element
{

    PlayerMovement movement;
    public float increasedRunSpeed;
    float baseRunSpeed;

    public override void Init()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        baseRunSpeed = movement.GetRunSpeed();
    }
    public override void PassiveOnSwitch()
    {
        movement.SetRunSpeed(increasedRunSpeed);
    }

    public override void PassiveOffSwitch()
    {
        movement.SetRunSpeed(baseRunSpeed);
    }

    public override void PassiveOnAbilitiy()
    {

    }

}
