using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;

    [Header("Events")]
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private event Action<HealthSystemUI> healthChanged;

    private bool isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
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

        currentHealth += amount;
        currentHealth = (int)Mathf.Clamp(currentHealth + amount, 0f, maxHealth);
    }

    void Death()
    {
        isDead = true;
        onDeath?.Invoke();
        Debug.Log("Death");
    }

    private void Update()
    {
        
    }
}
