using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Player/Base Values")]
    public class PlayerData : ScriptableObject
    {
        public float maxHealth = 3f;
        public float attackDamage = 10f;
    }
}
