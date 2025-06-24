using UnityEngine;

public class EnemyMageProjectileAttack : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private GameObject projectilePrefab;
    private Transform globalProjectileSpawnPoint;
    private void Start()
    {
        GameObject found = GameObject.Find("Mage Bolt");
        if (found != null)
            globalProjectileSpawnPoint= found.transform;
        else
        {
            Debug.LogWarning("Mage Bolt not found in the scene!");
        }
    }

    public void FireProjectile()
    {
        Vector3 spawnPosition;
        if (globalProjectileSpawnPoint != null)
            spawnPosition = globalProjectileSpawnPoint.position;
        else
            spawnPosition = new Vector3(0, transform.position.y, transform.position.z);

        GameObject projectileObj = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Projectile projectile = projectileObj.GetComponent<Projectile>();
        if (projectile != null)
        {
            Vector3 direction = -Vector3.forward;
            projectile.Init(direction, projectileData);
        }
    }
}