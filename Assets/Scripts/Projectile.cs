using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;

    private Vector3 moveDirection;
    private float speed;
    private Health health;

    void Start()
    {
        ProjectileManager.Instance.projectiles.Add(gameObject);
        health = GetComponent<Health>();
        health.Init(projectileData.maxHealth);
        health.OnDeath += OnDeath;
        speed = projectileData.speed;
    }

    public void Init(Vector3 direction, ProjectileData data)
    {
        projectileData = data;
        moveDirection = direction;
    }

    void Update()
    {
        transform.position += moveDirection * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(projectileData.damage);
            }

            Destroy(gameObject);
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}