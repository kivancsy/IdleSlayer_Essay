using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerData data;
    [SerializeField] Animator playerAnimator;

    public void Attack(GameObject target)
    {
        Debug.Log("Attacking " + target.name);
        var health = target.GetComponent<Health>();
        if (health != null || health.IsAlive())
        {
            playerAnimator.SetTrigger("castAttack");
            health.TakeDamage(data.attackDamage);
        }
    }
}