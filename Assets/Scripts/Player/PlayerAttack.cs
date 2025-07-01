using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] PlayerData data;
        [SerializeField] Animator playerAnimator;

        public void Attack(GameObject target)
        {
            var health = target.GetComponent<Health>();
            if (health != null || health.IsAlive())
            {
                playerAnimator.SetTrigger("castAttack");
                health.TakeDamage(data.attackDamage);
            }
        }
    }
}