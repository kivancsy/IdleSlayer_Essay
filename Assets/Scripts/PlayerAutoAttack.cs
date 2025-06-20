using UnityEngine;

public class PlayerAutoAttack : MonoBehaviour
{
    [SerializeField] private float attackRadius = 5f;
    [SerializeField] private float attackCoolDown = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] ProjectileData projectileData;

    [SerializeField] private float laneTolerance = 0.1f;

    // [SerializeField] private float[] lanes = { -2.5f, 0f, 2.5f };
    private float coolDownTimer;

    private void Update()
    {
        coolDownTimer -= Time.deltaTime;


        if (coolDownTimer <= 0)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                FireAt(target);
                coolDownTimer = attackCoolDown;
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;
        float playerX = transform.position.x;

        foreach (GameObject enemy in enemies)
        {
            float enemyX = enemy.transform.position.x;
            if (Mathf.Abs(enemyX - playerX) <= laneTolerance)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < nearestDistance && distance <= attackRadius)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }

    void FireAt(GameObject target)
    {
        Vector3 direction = (target.transform.position - projectileSpawnPoint.position).normalized + Vector3.up * 0.05f;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        var playerProjectile = projectile.GetComponent<PlayerProjectile>();
        if (playerProjectile != null)
            playerProjectile.Init(direction, projectileData);
    }
}