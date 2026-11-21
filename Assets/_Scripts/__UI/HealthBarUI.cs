
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [Header("Referências da HUD")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        maxHealth = Mathf.Max(1, maxHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        healthText.text = currentHealth + " / " + maxHealth;
    }
}
