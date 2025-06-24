using UnityEngine;

public class EnemySkeletonMage : BaseEnemy
{
    [SerializeField] private ObstacleSpawn obstacleSpawn;
    //[SerializeField] private ResourceType resourceType;

    protected override void Start()
    {
        base.Start();

        if (obstacleSpawn != null)
        {
            obstacleSpawn.StartSpawning();
        }
    }

    protected override void HandleDeath()
    {
        if (obstacleSpawn != null)
        {
            obstacleSpawn.StopSpawning();
        }

        //ResourceCollectManager.Instance.Collect(resourceType);
        base.HandleDeath();
    }
}