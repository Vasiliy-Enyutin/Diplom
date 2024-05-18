using System;
using _Project.Scripts.Descriptors;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.EnemyLogic
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class Enemy : MonoBehaviour, IDamageable
	{
		private int _health;
	    
		public EnemyDescriptor EnemyDescriptor { get; private set; }
		public GameObject Target { get; private set; }
		
		public MainBuilding MainBuilding { get; private set; }

		public event Action<Enemy> OnEnemyDied;
		
		public bool IsPursuingPlayer { get; set; }
		
		public void Init(GameObject player, EnemyDescriptor enemyDescriptor, MainBuilding mainBuilding)
		{
			Target = player;
			_health = enemyDescriptor.Health;
			EnemyDescriptor = enemyDescriptor;
			MainBuilding = mainBuilding;
		}

		public void TakeDamage(int damage)
		{
			_health -= damage;
			if (_health <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			OnEnemyDied?.Invoke(this);
			Destroy(gameObject);
		}
	}
}