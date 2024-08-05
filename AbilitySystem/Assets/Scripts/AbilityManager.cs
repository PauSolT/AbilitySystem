using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilities;
    public GameObject enemy;

    public GameObject player;

    ManagePlayerCooldowns uiPlayerCooldowns;
    private void Start()
    {
        enemy = FindObjectOfType<Enemy>().gameObject;
        player = FindObjectOfType<Player>().gameObject;
        uiPlayerCooldowns = GetComponent<ManagePlayerCooldowns>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseAbility(abilities[0]);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseAbility(abilities[1]);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseAbility(abilities[2]);
        }

        uiPlayerCooldowns.UpdateSlider1Cooldown(abilities[0].cooldown, abilities[0].GetRemainingCooldown());
        uiPlayerCooldowns.UpdateSlider2Cooldown(abilities[1].cooldown, abilities[1].GetRemainingCooldown());
        uiPlayerCooldowns.UpdateSlider3Cooldown(abilities[2].cooldown, abilities[2].GetRemainingCooldown());
    }


    public void UseAbility(Ability ability)
    {
        ability.UseAbility(player, enemy);
    }

}
