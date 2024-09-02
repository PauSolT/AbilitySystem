using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Element/Fire")]
public class FireElement : Element
{
    [SerializeField] private HealthComponent userHealth;
    [SerializeField] private float healPercentPerAbility = 5f;

    public override void Init()
    {
        userHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
    }
    public override void PassiveOnSwitch()
    {

    }

    public override void PassiveOffSwitch()
    {

    }

    public override void PassiveOnAbilitiy()
    {
        userHealth.Heal(userHealth.maxHealth * healPercentPerAbility / 100);
    }

}
