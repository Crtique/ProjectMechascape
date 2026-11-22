using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    private Animator anim;

    [SerializeField] private Canvas deathScreen;

    public Slider healthSlider;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        GameObject healthBarObj = GameObject.Find("HealthBar");
        if (healthBarObj != null)
        {
            healthSlider = healthBarObj.GetComponent<Slider>();
        }
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        Time.timeScale = 1.0f;
        deathScreen.enabled = false;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            GameOver();
            if(!CompareTag("Player"))
            {
                Destroy(this.gameObject, 3f);
            }
            Debug.Log("Dead");
        } 
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            Debug.Log("Health is full");
        }
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;

        deathScreen.enabled = true;
    }
}
