using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerElements : MonoBehaviour
{
    [SerializeField]
    int currentElement = 0;
    public List<Element> elements = new List<Element>();
    public List<Element> equippedElements = new List<Element>();

    ManagePlayerCooldowns uiPlayerCooldowns;

    [SerializeField]
    public static GameObject Enemy;

    bool ability1Pressed;
    bool ability2Pressed;
    bool ability3Pressed;
    bool cycleElementPressed;

    public static bool BlockedFromUsingAbilities = false;

    private void Start()
    {
        uiPlayerCooldowns = GetComponent<ManagePlayerCooldowns>();

        foreach (Element elem in elements)
        {
            elem.Init();
            foreach (Ability abil in elem.abilities)
            {
                abil.Init();
            }
        }
        elements[currentElement].elementState = ElementState.Active;
        uiPlayerCooldowns.UpdateSliderColors(GetCurrentElement().elementColor);
    }

    private void FixedUpdate()
    {
        UpdateSlidersCooldown();
        HandleInput();
        HandleCyclingElements();
        HandleUsingAbilities();
    }

    private void HandleInput()
    {
        if (BlockedFromUsingAbilities)
        {
            ability1Pressed = false;
            ability2Pressed = false;
            ability3Pressed = false;
            cycleElementPressed = false;
            return;
        };


        ability1Pressed = InputManager.Instance.GetAbility1Pressed();
        ability2Pressed = InputManager.Instance.GetAbility2Pressed();
        ability3Pressed = InputManager.Instance.GetAbility3Pressed();
        cycleElementPressed = InputManager.Instance.GetCycleElementPressed();
    }

    private void HandleCyclingElements()
    {
        elements[currentElement].elementState = ElementState.Equipped;
        if (cycleElementPressed)
        {
            currentElement++;
            if (currentElement >= elements.Count)
            {
                currentElement = 0;
            }
        }

        elements[currentElement].elementState = ElementState.Active;
        uiPlayerCooldowns.UpdateSliderColors(GetCurrentElement().elementColor);
        GetCurrentElement().PassiveOnSwitch();
    }

    private void UpdateSlidersCooldown()
    {
        if (uiPlayerCooldowns)
        {
            uiPlayerCooldowns.UpdateSlider1Cooldown(GetCurrentElement().abilities[0].GetRemainingCooldownNormalized());
            uiPlayerCooldowns.UpdateSlider2Cooldown(GetCurrentElement().abilities[1].GetRemainingCooldownNormalized());
            uiPlayerCooldowns.UpdateSlider3Cooldown(GetCurrentElement().abilities[2].GetRemainingCooldownNormalized());
        }
    }

    private void HandleUsingAbilities()
    {
        if (ability1Pressed)
        {
            UseAbility(GetCurrentElement().abilities[0]);
        }
        if (ability2Pressed)
        {
            UseAbility(GetCurrentElement().abilities[1]);
        }
        if (ability3Pressed)
        {
            UseAbility(GetCurrentElement().abilities[2]);
        }
    }

    public void UseAbility(Ability ability)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseToWorld.z = 0;

        switch (ability.targetingType)
        {
            default:
            case TargetingType.Mouse:
                ability.ActivateAbility(gameObject, mouseToWorld);
                break;
            case TargetingType.Self:
                ability.ActivateAbility(gameObject);
                break;
            case TargetingType.Targetted:
                if (Enemy)
                {
                    ability.ActivateAbility(gameObject);
                    Enemy = null;
                }
                break;
        }
    }

    private Element GetCurrentElement()
    {
        return elements[currentElement];
    }
}
