using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        //slider = gameObject.GetComponent<Slider>();
        slider = GameObject.Find("Health bar").GetComponent<Slider>();
    }

    public void SetMaxHealth(float health)
    {
        slider = GameObject.Find("Health bar").GetComponent<Slider>();
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider = GameObject.Find("Health bar").GetComponent<Slider>();
        slider.value = health;
    }

    public float GetHealth()
    {
        return slider.value;
    }
}
