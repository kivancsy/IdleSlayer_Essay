using System.Collections;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    [SerializeField] private float minWaitTime = 10f;
    [SerializeField] private float maxWaitTime = 20f;
    [SerializeField] private Transform spawnOffset;


    HealthPickupManager healthPickupManager;

    void Start()
    {
        StartCoroutine(SpawnResourcesRoutine());
    }

    IEnumerator SpawnResourcesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            if (Random.value < HealthPickupManager.Instance.healthSpawnChance)
            {
                HealthPickupManager.Instance.SpawnHealthPickup(spawnOffset);
            }
            else
            {
                ResourceManager.Instance.SpawnResource(spawnOffset);
            }
        }
    }
}