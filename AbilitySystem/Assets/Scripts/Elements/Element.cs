using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : ScriptableObject
{
    public ElementName elementName;
    ElementState elementState = ElementState.Unlocked;
    public List<Ability> abilities;

    public abstract void Passive();

}

public enum ElementName
{
    Fire,
    Water
}

public enum ElementState
{
    Active,
    Equipped,
    Unlocked,
    Locked
}