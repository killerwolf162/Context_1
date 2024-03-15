using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void set_max_health(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void set_health(float health)
    {
        slider.value = health;
    }
}
