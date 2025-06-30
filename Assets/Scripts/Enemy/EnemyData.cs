using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Base Values")]
public class EnemyData : ScriptableObject
{
    public string enemyName = "Default Enemy";
    public float maxHealth = 100f;
    public float damage = 1f;
}
