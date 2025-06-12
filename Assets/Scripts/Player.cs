using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Health health;

    public bool IsDead => !health.IsAlive();
    public float CurrentHealth => health.GetCurrentHealth();
    public float MaxHealth => playerData != null ? playerData.maxHealth : 0f;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Start()
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerData is not assigned in the Player script.");
            return;
        }

        health.Init(playerData.maxHealth);
        health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("Player has died.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        if (health != null)
            health.OnDeath -= HandleDeath;
    }

    public void Heal(float amount)
    {
        if (!IsDead)
            health.Heal(amount);
    }
    
    public void TakeDamage(float amount)
    {
        if (!IsDead)
            health.TakeDamage(amount);
    }
}