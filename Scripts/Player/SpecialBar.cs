using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    public Slider slider;

    public void set_special(float special_progress)
    {
        slider.value = special_progress;
    }
}

