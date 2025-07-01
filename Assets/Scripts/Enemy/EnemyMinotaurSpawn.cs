using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyMinotaurSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject minotaurPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnDelay = 5f;
        [SerializeField] private int maxMinotaurs = 1;
        [SerializeField] private float spawnChance = 0.05f;
        [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };

        [SerializeField] private int currentMinotaurCount = 0;

        private void Start()
        {
            StartCoroutine(SpawnMinotaurRoutine());
        }

        IEnumerator SpawnMinotaurRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);

                if (currentMinotaurCount >= maxMinotaurs)
                    continue;

                SpawnMinotaur();
            }
        }

        private void SpawnMinotaur()
        {
            if (Random.value > spawnChance || currentMinotaurCount >= maxMinotaurs)
                return;

            GameObject minotaur = Instantiate(
                minotaurPrefab,
                spawnPoint.position + new Vector3(lanes[Random.Range(0, lanes.Length)], 0.5f, 0),
                spawnPoint.rotation * Quaternion.Euler(0, 180f, 0),
                spawnPoint
            );
            Debug.Log("Spawned Minotaur");
            var enemy = minotaur.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.OnDeath += HandleMinotaurDeath;
            }

            var charge = minotaur.GetComponent<EnemyMinotaurCharge>();
            if (charge != null)
            {
                charge.OnDespawn += HandleMinotaurDeath;
            }

            currentMinotaurCount++;
        }

        private void HandleMinotaurDeath()
        {
            currentMinotaurCount--;
            Debug.Log("Handle Death Worked");
        }
    }
}