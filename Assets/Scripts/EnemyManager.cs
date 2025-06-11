using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    [SerializeField] private float spawnChance = 0.3f;

    private float minDistanceToCamera = 20f;
    public List<GameObject> enemies = new List<GameObject>();
    private Camera mainCamera;
    public int maxEnemyCount = 1;
    public List<int> availableLanes = new List<int> { 0, 1, 2 };

    public int ActiveEnemyCount =>
        enemies.Count; //Chatgpt'ye sor start'a yazsak farki ne - valuetype vs refarence type farki

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        mainCamera = Camera.main;
    }

    public void SpawnEnemy(Transform spawnOffset)
    {
        availableLanes = new List<int> { 0, 1, 2 };
        if (Random.value > spawnChance || availableLanes.Count <= 0 || ActiveEnemyCount >= maxEnemyCount)
            return;

        if (Vector3.Distance(mainCamera.transform.position, spawnOffset.position) < minDistanceToCamera)
            return;
        
        int laneIndex = SelectLane();
        var enemy = Instantiate(enemyPrefab, spawnOffset);
        enemy.transform.position = new Vector3(
            lanes[laneIndex],
            spawnOffset.position.y,
            spawnOffset.position.z);
        //enemies.Add(enemy);
    }

    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}