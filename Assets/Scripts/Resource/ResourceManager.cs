using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [SerializeField] GameObject resourcePrefab;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    [SerializeField] private float spawnChance = 0.5f;
    [SerializeField] private float spawnSeperationLenght = 2f;
    private float minDistanceToCamera = 30f;

    Camera mainCamera;
    List<int> availableLanes = new List<int> { 0, 1, 2 };
    public List<GameObject> resources = new List<GameObject>();
    public int maxResourceCount = 10;

    public int ActiveResourceCount => resources.Count;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        mainCamera = Camera.main;
    }

    public void SpawnResource(Transform spawnOffset)
    {
        availableLanes = new List<int> { 0, 1, 2 };
        if (Random.value > spawnChance || availableLanes.Count <= 0 || ActiveResourceCount >= maxResourceCount )
            return;
        int maxSpawnAmount = 6;
        int spawnAmount = Random.Range(5, maxSpawnAmount);
        float topOfSpawnPostionZ = spawnOffset.position.z + (spawnSeperationLenght * 2);
        int laneIndex = SelectLane();

        for (int i = 0; i < spawnAmount; i++)
        {
            float spawnPositionZ = topOfSpawnPostionZ - (i * spawnSeperationLenght);
            Vector3 spawnPos = new Vector3(
                lanes[laneIndex],
                spawnOffset.position.y,
                spawnPositionZ
            );
            
            if (Vector3.Distance(mainCamera.transform.position, spawnPos) < minDistanceToCamera)
                continue;

            var resource = Instantiate(resourcePrefab, spawnOffset);
            resource.transform.position = spawnPos;
            //resources.Add(resource);
        }
    }

    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }


    public void RemoveResource(GameObject resource)
    {
        resources.Remove(resource);
    }
}