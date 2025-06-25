using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private float lifetime = 5f;

    private Vector3 moveDirection;
    private float speed;
    private float lifeTimer;
    private Health health;
    private Player player;

    void Start()
    {
        ProjectileManager.Instance.projectiles.Add(gameObject);
        health = GetComponent<Health>();
        health.Init(projectileData.maxHealth);
        //health.OnDeath += OnDeath;
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
        
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerHealth = other.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.PlayerTakeDamage(projectileData.damage);
            }

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ProjectileManager.Instance.projectiles.Remove(gameObject);
    }
    // private void OnDeath()
    // {
    //     Destroy(gameObject);
    // }
}