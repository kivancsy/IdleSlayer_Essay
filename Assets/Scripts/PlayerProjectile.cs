using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private float lifetime = 5f;

    private Vector3 moveDirection;
    private float speed;
    private float lifeTimer;

    void Start()
    {
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
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(projectileData.damage);
            }
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player") && !other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}