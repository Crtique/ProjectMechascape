using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;

    [Header("Events")]
    [SerializeField] private UnityEvent onDeath;

    public UnityEvent onHealthChanged;

    private bool isDead;

    // Expression-Bodied Properties to access our private health and boolean variables
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => isDead;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke();
    }

    public void TakeDamage(float amount)
    {
        if (isDead || amount <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);


        if (currentHealth <= 0f)
            Death();
    }

    public void Heal(float amount)
    {
        if (isDead || amount <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
    }

    void Death()
    {
        isDead = true;
        onDeath?.Invoke();
        Debug.Log("Death");
    }
}
