using _Project.Scripts.EnemyLogic;
using UnityEngine;

namespace _Project.Scripts.Descriptors
{
    [CreateAssetMenu(fileName = "EnemiesSpawnerDescriptor", menuName = "Descriptors/EnemiesSpawner", order = 0)]
    public class EnemyDescriptor : ScriptableObject
    {
        public Enemy Enemy;
        public int Health;
        public int EnemiesNumber;
        public float MoveSpeed;
        public float PursuitDistance;
        public int Damage;
        public float AttackDuration;
        public float ReloadSpeed;
    }
}
