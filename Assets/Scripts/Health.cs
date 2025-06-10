using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    public event Action OnDeath;
    public void Init(float maxHealthValue)
    {
        this.maxHealth = maxHealthValue;
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}