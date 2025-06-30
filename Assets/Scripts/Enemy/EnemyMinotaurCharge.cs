// Assets/Scripts/Enemy/EnemyMinotaurCharge.cs

using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyMinotaurCharge : MonoBehaviour
    {
        [SerializeField] private float chargeSpeed = 5f;
        [SerializeField] private Animator animator;
        [SerializeField] private float despawnZ = -5f;

        private bool isDead = false;
        public event Action OnDespawn;

        void Update()
        {
            if (!isDead)
                transform.position += -Vector3.forward * (chargeSpeed * Time.deltaTime);

            if (transform.position.z < despawnZ)
            {
                OnDespawn?.Invoke();
                Destroy(gameObject);
            }
        }

        public void PlayDeathAnimation()
        {
            if (animator != null)
                animator.SetTrigger("isDead");
            isDead = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isDead) return;
            Debug.Log($"Çarpışan tag: {other.tag}, isim: {other.name}");
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<Player>();
                var baseEnemy = GetComponent<BaseEnemy>();
                if (player != null && baseEnemy != null && baseEnemy.EnemyData != null)
                {
                    player.PlayerTakeDamage(baseEnemy.EnemyData.damage);
                }
            }
            else if (other.CompareTag("EnemyMelee") || other.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}