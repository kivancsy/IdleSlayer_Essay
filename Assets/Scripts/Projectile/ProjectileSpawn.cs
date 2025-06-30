using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpawnTime = 1f;
    [SerializeField] private Transform spawnOffset;

    private void Start()
    {
        StartCoroutine(SpawnProjectileRoutine());
    }

    IEnumerator SpawnProjectileRoutine()
    {
        yield return new WaitForSeconds(projectileSpawnTime);
        while (true)
        {
            ProjectileManager.Instance.SpawnProjectileAtCastle(spawnOffset);
            yield return new WaitForSeconds(projectileSpawnTime);
        }
    }
}