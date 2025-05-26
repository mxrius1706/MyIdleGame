using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthSlider;
    public GameObject player; // Assign the player GameObject in the Inspector
    private Damageable damageable;
    public TMP_Text healthText;

    void Start()
    {
        if (player != null)
        {
            damageable = player.GetComponent<Damageable>();
        }
        if (damageable != null && healthSlider != null)
        {
            healthSlider.maxValue = damageable.MaxHealth;
            healthSlider.value = damageable.Health;
        }
        if (healthText == null)
        {
            healthText = GetComponentInChildren<TMP_Text>();
        }
    }

    void Update()
    {
        if (damageable != null && healthSlider != null)
        {
            healthSlider.value = damageable.Health;
            if (healthText != null)
                healthText.text = "Health " + $"{damageable.Health} / {damageable.MaxHealth}";

           
            ColorBlock cb = healthSlider.colors;
            if (damageable.Health <= 20)
            {
                cb.normalColor = Color.red;
            }
            else
            {
                cb.normalColor = Color.green;
            }
            healthSlider.colors = cb;
        }
    }
}
