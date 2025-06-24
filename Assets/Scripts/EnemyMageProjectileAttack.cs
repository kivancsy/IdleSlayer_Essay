using UnityEngine;
using System.Collections;

public class EnemyMageProjectileAttack : MonoBehaviour
{
    [SerializeField] private ProjectileData projectileData;
    [SerializeField] private float fireInterval = 2f;
    private Transform globalProjectileSpawnPoint;
    private Coroutine fireRoutine;

    private void Start()
    {
        GameObject found = GameObject.Find("Mage Bolt");
        if (found != null)
            globalProjectileSpawnPoint = found.transform;
        else
            Debug.LogWarning("Mage Bolt not found in the scene!");
    }

    public void StartFiring()
    {
        if (fireRoutine == null)
            fireRoutine = StartCoroutine(FireRoutine());
    }

    public void StopFiring()
    {
        if (fireRoutine != null)
            StopCoroutine(fireRoutine);
        fireRoutine = null;
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            FireProjectile();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    public void FireProjectile()
    {
        if (projectileData == null || projectileData.prefab == null)
        {
            Debug.LogWarning("ProjectileData veya prefab atanmamış!");
            return;
        }

        Vector3 spawnPosition = globalProjectileSpawnPoint != null
            ? globalProjectileSpawnPoint.position
            : new Vector3(0, transform.position.y, transform.position.z);

        GameObject projectileObj = Instantiate(projectileData.prefab, spawnPosition, Quaternion.identity);

        Projectile projectile = projectileObj.GetComponent<Projectile>();
        if (projectile != null)
        {
            Vector3 direction = -Vector3.forward;
            projectile.Init(direction, projectileData);
        }
    }
}