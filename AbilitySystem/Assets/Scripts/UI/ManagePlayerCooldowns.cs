using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePlayerCooldowns : MonoBehaviour
{
    public Slider ability1;
    public Slider ability2;
    public Slider ability3;

    public void UpdateSlider1Cooldown(float normalizedCooldown)
    {
        ability1.value = normalizedCooldown;
    }
    public void UpdateSlider2Cooldown(float normalizedCooldown)
    {
        ability2.value = normalizedCooldown;
    }

    public void UpdateSlider3Cooldown(float normalizedCooldown)
    {
        ability3.value = normalizedCooldown;
    }

}
