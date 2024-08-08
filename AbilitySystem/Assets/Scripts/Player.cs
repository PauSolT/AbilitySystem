using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int currentElement = 0;
    public List<Element> elements = new List<Element>();

    ManagePlayerCooldowns uiPlayerCooldowns;
    GameObject enemy;

    private void Start()
    {
        uiPlayerCooldowns = GetComponent<ManagePlayerCooldowns>();
        enemy = FindObjectOfType<Enemy>().gameObject;
    }

    void Update()
    {
        UpdateSlidersCooldown();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseAbility(GetCurrentElement().abilities[0]);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseAbility(GetCurrentElement().abilities[1]);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseAbility(GetCurrentElement().abilities[2]);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            NextElement();
        }

    }

    private void NextElement()
    {
        currentElement++;
        if (currentElement >= elements.Count)
        {
            currentElement = 0;
        }
    }

    private void UpdateSlidersCooldown()
    {
        uiPlayerCooldowns.UpdateSlider1Cooldown(GetCurrentElement().abilities[0].GetRemainingCooldownNormalized());
        uiPlayerCooldowns.UpdateSlider2Cooldown(GetCurrentElement().abilities[1].GetRemainingCooldownNormalized());
        uiPlayerCooldowns.UpdateSlider3Cooldown(GetCurrentElement().abilities[2].GetRemainingCooldownNormalized());
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
