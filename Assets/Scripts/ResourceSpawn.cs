using System.Collections;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{
    [SerializeField] private float minWaitTime = 10f;
    [SerializeField] private float maxWaitTime = 20f;
    [SerializeField] private Transform spawnOffset;

    void Start()
    {
        StartCoroutine(SpawnResourcesRoutine());
    }

    IEnumerator SpawnResourcesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            ResourceManager.Instance.SpawnResource(spawnOffset);
        }
    }
}