using UnityEngine;

namespace Enemy
{
    public class EnemyMinotaur : BaseEnemy
    {
        [SerializeField] private EnemyMinotaurCharge minotaurCharge;
        [SerializeField] private ResourceType resourceType;

        protected override void Start()
        {
            base.Start();
            
        }

        protected override void HandleDeath()
        {
            if (minotaurCharge != null)
            {
                minotaurCharge.PlayDeathAnimation();
            }

            ResourceCollectManager.Instance.Collect(resourceType);
            base.HandleDeath();
        }
    }
}