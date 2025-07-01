using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerAutoAttack : MonoBehaviour
    {
        [SerializeField] private float attackRadius = 5f;
        [SerializeField] private float attackCoolDown = 1f;
        [SerializeField] private float reloadTime = 1.5f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] ProjectileData projectileData;
        [SerializeField] private float laneTolerance = 0.1f;
        [SerializeField] private int maxAmmo = 3;

        private float coolDownTimer;
        private int currentAmmo;
        private bool isReloading = false;

        private void Start()
        {
            currentAmmo = maxAmmo;
        }

        private void Update()
        {
            if (isReloading)
                return;

            coolDownTimer -= Time.deltaTime;

            if (coolDownTimer <= 0 && currentAmmo > 0)
            {
                GameObject target = FindNearestEnemy();
                if (target != null)
                {
                    FireAt(target);
                    currentAmmo--;
                    coolDownTimer = attackCoolDown;

                    if (currentAmmo <= 0)
                    {
                        StartCoroutine(Reload());
                    }
                }
            }
        }

        IEnumerator Reload()
        {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            isReloading = false;
        }

        GameObject FindNearestEnemy()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearestEnemy = null;
            float nearestDistance = Mathf.Infinity;
            float playerX = transform.position.x;

            foreach (GameObject enemy in enemies)
            {
                float enemyX = enemy.transform.position.x;
                if (Mathf.Abs(enemyX - playerX) <= laneTolerance)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < nearestDistance && distance <= attackRadius)
                    {
                        nearestDistance = distance;
                        nearestEnemy = enemy;
                    }
                }
            }

            return nearestEnemy;
        }

        void FireAt(GameObject target)
        {
            Vector3 direction = (target.transform.position - projectileSpawnPoint.position).normalized + Vector3.up * 0.05f;

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            var playerProjectile = projectile.GetComponent<PlayerProjectile>();
            if (playerProjectile != null)
                playerProjectile.Init(direction, projectileData);
        }
    }
}