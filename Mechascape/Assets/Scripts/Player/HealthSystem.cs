using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] float maxhealth = 100f;
    [SerializeField] float currentHealth;

    [Header("UI Health Settings")]
    [SerializeField] Slider healthSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxhealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxhealth);

        UpdateHealthBar();

        if (currentHealth <= 0f)
            Death();
    }

    void UpdateHealthBar()
    {

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxhealth;
            healthSlider.value = currentHealth;
        }
    }

    void Death()
    {
        Debug.Log("Death");
    }
}
