// Assets/Scripts/HealthPickUp.cs
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private float healthAmount = 1f;

    private void Start()
    {
        HealthPickupManager.Instance?.AddHealthPickup(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Player playerHealth = other.GetComponent<Player.Player>();
            if (playerHealth != null)
            {
                playerHealth.PlayerHeal(healthAmount);
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("EnemyMelee"))
        {
            EnemyManager.Instance.enemies.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Coin"))
        {
            ResourceManager.Instance.RemoveResource(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        HealthPickupManager.Instance?.RemoveHealthPickup(gameObject);
    }
}