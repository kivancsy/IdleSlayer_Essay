using UnityEngine;

public class PlayerAutoAttack : MonoBehaviour
{
    [SerializeField] private float attackRadius = 5f;
    [SerializeField] private float attackCoolDown = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] ProjectileData projectileData;

    private float coolDownTimer;

    private void Update()
    {
        coolDownTimer -= Time.deltaTime;

        GameObject target = FindNearestEnemy();
        if (target != null && coolDownTimer <= 0f)
        {
            FireAt(target);
            coolDownTimer = attackCoolDown;
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance && distance <= attackRadius)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
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