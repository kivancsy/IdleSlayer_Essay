using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public EnemyData Data;
    [SerializeField] public ResourceType resourceType;
    private Health health;

    private void Start()
    {
        EnemyManager.Instance.enemies.Add(gameObject);
        health = GetComponent<Health>();
        health.Init(Data.maxHealth);
        health.OnDeath += OnDeath;
        Debug.Log($"Max Health: {Data.maxHealth}, Current Health: {health.GetCurrentHealth()}");
    }

    private void OnDeath()
    {
        ResourceCollectManager.Instance.Collect(resourceType);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        EnemyManager.Instance.enemies.Remove(gameObject);
    }
}