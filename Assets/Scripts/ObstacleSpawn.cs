using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> obstaclePrefabs = new List<GameObject>();
    [SerializeField] float obstaclesSpawnTime = 1f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 3f;
    [SerializeField] float spawnHeight = 3f;


    void Start()
    {
        StartCoroutine(SpawnObstaclesRoutine());
    }

    IEnumerator SpawnObstaclesRoutine()
    {
        while (true)
        {
            GameObject selectedObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
            // Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            // Vector3 spawnPositionHeight = new Vector3(Random.Range(-spawnHeight, spawnHeight), transform.position.y, transform.position.z);
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnWidth, spawnWidth),
                Random.Range(spawnHeight, spawnHeight * 2),
                transform.position.z
            );
            yield return new WaitForSeconds(obstaclesSpawnTime);
            Instantiate(selectedObstacle, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}