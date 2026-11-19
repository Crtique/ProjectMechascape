using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [Header("Enemy Health SettingS")]
    [SerializeField] private float currentEnemyHealth; // Get the current health of the enemy
    [SerializeField] private float maxEnemyHealth;     // Set the max health of that enemy


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;
    }

    public void EnemyTakeDamage(float amount)
    {
        currentEnemyHealth -= amount;
        currentEnemyHealth = Mathf.Clamp(currentEnemyHealth, 0, maxEnemyHealth);

        if (currentEnemyHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }
}
