using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fillImage;
    
    public void SetMaxBarValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;

        fillImage.color = gradient.Evaluate(1f);
    }
    public void SetBarValue(int value)
    {
        slider.value = value;

        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }
}
