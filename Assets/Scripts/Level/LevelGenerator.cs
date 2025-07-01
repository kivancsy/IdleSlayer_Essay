using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab;
    [SerializeField] int startingGroundsAmount = 12;
    [SerializeField] Transform environmentParent;
    [SerializeField] float groundLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] List<GameObject> grounds = new List<GameObject>();
    [SerializeField] PlayerController player;
    [SerializeField] Animator playerAnimator;
    bool hasStartedRunning = false;
    Camera mainCamera;

    void Start()
    {
        GenerateGround();
    }

    void Update()
    {
        MoveLane();
    }

    private void GenerateGround()
    {
        for (int i = 0; i < startingGroundsAmount; i++)
        {
            SpawnInitialLane();
        }
    }

    private void SpawnInitialLane()
    {
        SpawnLane();
    }

    private void SpawnLane()
    {
        float spawnPositionZ = SetSpawnPositionZ();

        Vector3 groundSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject newGround = Instantiate(groundPrefab, groundSpawnPos, Quaternion.identity, environmentParent);
        grounds.Add(newGround);
    }

    float SetSpawnPositionZ()
    {
        float spawnPositionZ;

        if (grounds.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = grounds[grounds.Count - 1].transform.position.z + groundLength;
        }

        return spawnPositionZ;
    }

    void MoveLane()
    {
        if (player != null && OnPlayerInput())
        {
            for (int i = 0; i < grounds.Count; i++)
            {
                GameObject ground = grounds[i];
                ground.transform.Translate(-Vector3.forward * (moveSpeed * Time.deltaTime));

                if (ground.transform.position.z < Camera.main.transform.position.z - groundLength)
                {
                    grounds.RemoveAt(i);
                    Destroy(ground);
                    SpawnLane();
                }
            }
        }
    }
    public bool OnPlayerInput()
    {
        if (!hasStartedRunning && Keyboard.current.wKey.wasPressedThisFrame)
        {
            playerAnimator.SetBool("isRunning", true);
            hasStartedRunning = true;
        }

        return hasStartedRunning;
    }
}