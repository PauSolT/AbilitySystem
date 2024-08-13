using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElements : MonoBehaviour
{
    [SerializeField]
    int currentElement = 0;
    public List<Element> elements = new List<Element>();
    public List<Element> equippedElements = new List<Element>();

    ManagePlayerCooldowns uiPlayerCooldowns;
    GameObject enemy;

    bool ability1Pressed;
    bool ability2Pressed;
    bool ability3Pressed;
    bool cycleElementPressed;

    private void Start()
    {
        uiPlayerCooldowns = GetComponent<ManagePlayerCooldowns>();
        enemy = FindObjectOfType<Enemy>().gameObject;

        foreach (Element elem in elements)
        {
            elem.Init();
            foreach (Ability abil in elem.abilities)
            {
                abil.Init();
            }
        }

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
        ability1Pressed = InputManager.Instance.GetAbility1Pressed();
        ability2Pressed = InputManager.Instance.GetAbility2Pressed();
        ability3Pressed = InputManager.Instance.GetAbility3Pressed();
        cycleElementPressed = InputManager.Instance.GetCycleElementPressed();
    }

    private void HandleCyclingElements()
    {
        if (cycleElementPressed)
        {
            currentElement++;
            if (currentElement >= elements.Count)
            {
                currentElement = 0;
            }
        }

        uiPlayerCooldowns.UpdateSliderColors(GetCurrentElement().elementColor);
        GetCurrentElement().PassiveSwitch();
    }

    private void UpdateSlidersCooldown()
    {
        uiPlayerCooldowns.UpdateSlider1Cooldown(GetCurrentElement().abilities[0].GetRemainingCooldownNormalized());
        uiPlayerCooldowns.UpdateSlider2Cooldown(GetCurrentElement().abilities[1].GetRemainingCooldownNormalized());
        uiPlayerCooldowns.UpdateSlider3Cooldown(GetCurrentElement().abilities[2].GetRemainingCooldownNormalized());
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
        ability.UseAbility(gameObject, enemy);
    }

    private Element GetCurrentElement()
    {
        return elements[currentElement];
    }
}
