using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Element/Fire")]
public class FireElement : Element
{
    [SerializeField] private float healPercentPerAbility = 5f;
    public override void Passive()
    {

    }

}
