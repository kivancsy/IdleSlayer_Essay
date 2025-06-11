using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Base Values")]
public class ProjectileData : ScriptableObject
{
    public string projectileName = "DemonBolt";
    public float maxHealth = 1f;
    public float damage = 1f;
    public float speed = 10f;
    public GameObject prefab;
}