using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValue(float maxHealth) //fraction
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetValue(float health) //fraction
    {
        slider.value = health;
    }
}
