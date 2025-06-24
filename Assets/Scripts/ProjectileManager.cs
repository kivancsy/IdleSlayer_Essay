using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float spawnChance = 0.5f;
    [SerializeField] ProjectileData projectileData;
    [SerializeField] Transform target;
    [SerializeField] Transform spawnParent;
    [SerializeField] public List<GameObject> projectiles = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnProjectileAtCastle(Transform spawnOffset)
    {
        if (!(Random.value < spawnChance)) return;
        GameObject projectile = Instantiate(projectilePrefab, spawnOffset.position, Quaternion.identity, spawnParent);

        
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null && target != null && projectileData != null)
        {
            Vector3 targetPosition = target.position + Vector3.up * 2f;
            Vector3 direction = (targetPosition - spawnOffset.position).normalized;
            projectileScript.Init(direction, projectileData);
        }
    }
}