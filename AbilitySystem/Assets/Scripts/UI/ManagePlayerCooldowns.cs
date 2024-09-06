using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePlayerCooldowns : MonoBehaviour
{
    public Slider ability1;
    public Slider ability2;
    public Slider ability3;

    [SerializeField]
    List<Image> sliderImages = new List<Image>();

    private void Start()
    {
        sliderImages.Add(ability1.transform.GetChild(1).GetChild(0).GetComponent<Image>());
        sliderImages.Add(ability2.transform.GetChild(1).GetChild(0).GetComponent<Image>());
        sliderImages.Add(ability3.transform.GetChild(1).GetChild(0).GetComponent<Image>());
    }
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

    public void UpdateSliderColors(Color32 color)
    {
        foreach (Image img in sliderImages)
        {
            img.color = color;
        }
    }

}
