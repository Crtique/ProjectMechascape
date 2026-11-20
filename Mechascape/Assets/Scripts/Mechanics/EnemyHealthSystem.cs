using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [Header("Enemy Health SettingS")]
    [SerializeField] private float currentEnemyHealth; // Get the current health of the enemy
    [SerializeField] private float maxEnemyHealth;     // Set the max health of that enemy

    [Header("Item Drops")]
    [SerializeField] private GameObject[] pickUpDrops;  // Array for Pickups
    private readonly float dropChance = 0.7f;           // Drop percentage

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
            DropPickUps();
            Debug.Log("Dead");
        }
    }

    void DropPickUps()
    {
        if (pickUpDrops.Length == 0) return;

        if (Random.value <= dropChance && pickUpDrops.Length > 0)
        {
            int index = Random.Range(0, pickUpDrops.Length);
            Instantiate(pickUpDrops[index], transform.position, Quaternion.identity);
        }
    }
}
