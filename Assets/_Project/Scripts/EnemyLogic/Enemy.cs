using _Project.Scripts.Descriptors;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
	    private int _health;
	    
	    public EnemyDescriptor EnemyDescriptor { get; private set; }
        public GameObject Target { get; private set; }
        
        public bool IsPursuingPlayer { get; set; }

        public void Init(GameObject player, EnemyDescriptor enemyDescriptor)
        {
            Target = player;
            _health = enemyDescriptor.Health;
            EnemyDescriptor = enemyDescriptor;
        }

        public void TakeDamage(int damage)
        {
	        Debug.Log($"Enemy take damage {damage}");
	        _health -= damage;
	        if (_health <= 0)
	        {
		        Die();
	        }
        }

        private void Die()
        {
	        Destroy(gameObject);
        }
    }
}
