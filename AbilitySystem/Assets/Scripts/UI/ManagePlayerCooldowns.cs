using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePlayerCooldowns : MonoBehaviour
{
    public Slider ability1;
    public Slider ability2;
    public Slider ability3;

    public void UpdateSlider1Cooldown(float baseCooldown, float currentCooldown)
    {
        ability1.value = (baseCooldown - currentCooldown) / baseCooldown;
    }
    public void UpdateSlider2Cooldown(float baseCooldown, float currentCooldown)
    {
        ability2.value = (baseCooldown - currentCooldown) / baseCooldown;
    }

    public void UpdateSlider3Cooldown(float baseCooldown, float currentCooldown)
    {
        ability3.value = (baseCooldown - currentCooldown) / baseCooldown;
    }

}
