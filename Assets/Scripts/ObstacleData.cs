using UnityEngine;

[CreateAssetMenu(menuName = "Obstacle/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public float maxHealth = 1;
    public float damage = 1;
    public float launchForce = 10f;
}