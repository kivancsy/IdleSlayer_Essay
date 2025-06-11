using UnityEngine;

public class ProjectileEnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private ProjectileData projectileData;

    public void Attack(Transform target)
    {
        if (projectilePrefab == null || target == null || projectileData == null)
            return;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            projectileScript.Init(direction, projectileData);
        }
    }
}