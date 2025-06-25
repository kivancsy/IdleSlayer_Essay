using UnityEngine;

public class EnemySkeletonMage : BaseEnemy
{
    [SerializeField] private ObstacleSpawn obstacleSpawn;
    [SerializeField] private EnemyMageProjectileAttack mageProjectileAttack;
    [SerializeField] private ResourceType resourceType;

    protected override void Start()
    {
        base.Start();

        if (obstacleSpawn != null)
        {
            obstacleSpawn.StartSpawning();
        }

        if (mageProjectileAttack != null)
            mageProjectileAttack.StartFiring();
    }

    protected override void HandleDeath()
    {
        if (obstacleSpawn != null)
        {
            obstacleSpawn.StopSpawning();
        }

        if (mageProjectileAttack != null)
            mageProjectileAttack.StopFiring();
        ResourceCollectManager.Instance.Collect(resourceType);
        base.HandleDeath();
    }
}