using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    public float maxHealth = 100f;
    private float currentHealth;
    public bool isPlayer = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        Debug.Log($"{gameObject.name} took {amount} damage.");
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log((isPlayer ? "Player" : "Enemy") + " has died.");
        if (isPlayer)
        {
            // EventManager.Instance.PlayerEvents.OnDeath();
            // Possibly trigger a game over screen or other logic
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pink"))
        {
            TakeDamage(15);
        }
        else if (other.CompareTag("Yellow"))
        {
            TakeDamage(10);
        }
        else if (other.CompareTag("Red"))
        {
            TakeDamage(5);
        }
        else
        {
            TakeDamage(10);
        }
    }
}