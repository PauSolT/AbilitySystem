using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Element/Earth")]
public class EarthElement : Element
{
    [SerializeField] private GameObject user;
    [SerializeField] private float shieldAmount = 10f;
    [SerializeField] private float duration = 10f;

    public override void Init()
    {
        user = GameObject.FindGameObjectWithTag("Player");
    }
    public override void PassiveOnSwitch()
    {
    }

    public override void PassiveOffSwitch()
    {

    }

    public override void PassiveOnAbilitiy()
    {
        user.AddComponent<ShieldComponent>().Init(shieldAmount, duration);
    }

}
