using System.Collections.Generic;
using UnityEngine;

public class HealthPickupManager : MonoBehaviour
{
    public static HealthPickupManager Instance { get; private set; }

    [SerializeField] private GameObject healthPickupPrefab;
    [SerializeField] private float[] lanes = { -2.5f, 0f, 2.5f };
    [SerializeField] public float healthSpawnChance = 0.2f;
    [SerializeField] private int maxActiveHealthPickups = 1;
    private float minDistanceToCamera = 30f;

    private List<GameObject> activeHealthPickups = new List<GameObject>();
    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        mainCamera = Camera.main;
    }

    public void SpawnHealthPickup(Transform spawnOffset)
    {
        if (activeHealthPickups.Count >= maxActiveHealthPickups)
            return;

        int laneIndex = Random.Range(0, lanes.Length);
        Vector3 spawnPos = new Vector3(
            lanes[laneIndex],
            spawnOffset.position.y,
            spawnOffset.position.z
        );
        if (Vector3.Distance(mainCamera.transform.position, spawnPos) < minDistanceToCamera)
            return;
        var healthPickup = Instantiate(healthPickupPrefab, spawnOffset);
        healthPickup.transform.position = spawnPos;
    }

    public void AddHealthPickup(GameObject pickup)
    {
        if (!activeHealthPickups.Contains(pickup))
            activeHealthPickups.Add(pickup);
    }

    public void RemoveHealthPickup(GameObject pickup)
    {
        activeHealthPickups.Remove(pickup);
    }

    public int ActiveHealthPickupCount => activeHealthPickups.Count;
}