using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySkeletonMageSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> magePrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private int maxMages = 1;
    [SerializeField] private float spawnChance = 0.05f;

    private int currentMageCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnMageRoutine());
    }

    IEnumerator SpawnMageRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            if (currentMageCount >= maxMages)
                continue;

            SpawnMage();
        }
    }

    private void SpawnMage()
    {
        if (Random.value > spawnChance || currentMageCount >= maxMages)
            return;
        GameObject mage = Instantiate(magePrefabs[Random.Range(0, magePrefabs.Count)], spawnPoint.position,
            spawnPoint.rotation, spawnPoint);

        var enemy = mage.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            enemy.OnDeath += HandleMageDeath;
        }

        currentMageCount++;
    }

    private void HandleMageDeath()
    {
        currentMageCount--;
    }
}