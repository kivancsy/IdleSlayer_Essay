using System;
using UnityEngine;

public class CastleGenerator : MonoBehaviour
{
    [SerializeField] GameObject castlePrefab;
    [SerializeField] Transform environmentParent;
    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float xClamp = 1f;
    [SerializeField] float yClamp = 1f;
    [SerializeField] float zClamp = 1f;

    GameObject spawnedCastle;
    Vector3 startPosition;

    void Start()
    {
        CastleSpawn();
    }

    void Update()
    {
        CastleMovement();
    }

    void CastleSpawn()
    {
        spawnedCastle = Instantiate(castlePrefab, transform.position, Quaternion.identity, environmentParent);
        startPosition = spawnedCastle.transform.position;
    }

    void CastleMovement()
    {
        if (spawnedCastle == null) return;

        float x = startPosition.x + Mathf.Sin(Time.time * moveSpeed);
        float y = startPosition.y + Mathf.Sin(Time.time * moveSpeed) ;
        float z = startPosition.z + Mathf.Sin(Time.time * moveSpeed);
        
        x = Mathf.Clamp(x, startPosition.x - xClamp, startPosition.x + xClamp);
        y = Mathf.Clamp(y, startPosition.y - yClamp, startPosition.y + yClamp);
        z = Mathf.Clamp(z, startPosition.z - zClamp, startPosition.z + zClamp);

        spawnedCastle.transform.position = new Vector3(x, y, z);

        float rotationAmount = Mathf.Sin(Time.time * rotationSpeed);
        spawnedCastle.transform.rotation = Quaternion.Euler(rotationAmount, rotationAmount, rotationAmount);
    }
}