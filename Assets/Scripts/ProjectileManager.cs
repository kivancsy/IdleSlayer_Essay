using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float spawnChance = 0.5f;
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
        GameObject selectedProjectile = projectilePrefab;
        Vector3 spawnPosition =
            new Vector3(spawnOffset.position.x, spawnOffset.position.y, spawnOffset.position.z);
        Instantiate(selectedProjectile, spawnPosition, Quaternion.identity);
    }
}