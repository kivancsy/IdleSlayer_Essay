using UnityEngine;

public class EnemySkeletonWarrior : BaseEnemy
{
    [SerializeField] public ResourceType resourceType;

    protected override void HandleDeath()
    {
        base.HandleDeath();
        
        ResourceCollectManager.Instance.Collect(resourceType);
    }
}