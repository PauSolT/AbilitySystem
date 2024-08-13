using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public override void PassiveSwitch()
    {

    }

    public override void PassiveAbilitiy()
    {
        userHealth.Heal(userHealth.maxHealth * healPercentPerAbility / 100);
    }

}
