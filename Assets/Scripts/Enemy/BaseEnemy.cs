using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    public EnemyData EnemyData => enemyData;

    protected Health health;

    public event Action OnDeath;

    protected virtual void Start()
    {
        health = GetComponent<Health>();
        if (enemyData == null)
        {
            Debug.LogError("EnemyData is not assigned in " + gameObject.name);
            return;
        }

        health.Init(enemyData.maxHealth);
        health.OnDeath += HandleDeath;

        EnemyManager.Instance.enemies.Add(gameObject);
    }

    protected virtual void HandleDeath()
    {
        health.OnDeath -= HandleDeath;
        OnDeath?.Invoke();
        EnemyManager.Instance.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (EnemyManager.Instance != null)
            EnemyManager.Instance.enemies.Remove(gameObject);
    }
}