using _Project.Scripts.EnemyLogic;
using UnityEngine;

namespace _Project.Scripts.Descriptors
{
    [CreateAssetMenu(fileName = "EnemiesSpawnerDescriptor", menuName = "Descriptors/EnemiesSpawner", order = 0)]
    public class EnemyDescriptor : ScriptableObject
    {
        public Enemy Enemy;
        public float Health;
        public int EnemiesNumber;
        public float MoveSpeed;
        public float PursuitDistance;
        public float Damage;
        public float AttackDuration;
        public float ReloadSpeed;
    }
}
