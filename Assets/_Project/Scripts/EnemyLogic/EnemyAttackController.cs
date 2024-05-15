using System;
using System.Collections;
using _Project.Scripts.Descriptors;
using _Project.Scripts.PlayerLogic;
using UnityEngine;

namespace _Project.Scripts.EnemyLogic
{
	public class EnemyAttackController : MonoBehaviour
	{
		[SerializeField]
		private SphereCollider _weaponCollider;

		public event Action OnAttacking;
		
		private EnemyMovement _enemyMovement;

		private float _damage;
		private float _attackDuration;
		private float _reloadSpeed;
		private bool _isReadyToAttack = true;

		private void Start()
		{
			_weaponCollider.enabled = false;
			_enemyMovement = GetComponent<EnemyMovement>();
			EnemyDescriptor enemyDescriptor = GetComponent<Enemy>().EnemyDescriptor;
			_damage = enemyDescriptor.Damage;
			_attackDuration = enemyDescriptor.AttackDuration;
			_reloadSpeed = enemyDescriptor.ReloadSpeed;
		}

		private void Update()
		{
			
			if (_enemyMovement.EnemyCanAttack && _isReadyToAttack)
			{
				OnAttacking?.Invoke();
				PerformAttack();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out Player player))
			{
				player.TakeDamage(_damage);
			}
		}

		private void PerformAttack()
		{
			StartCoroutine(ReloadRoutine());
			StartCoroutine(TemporarilyEnableColliderRoutine());
			StartCoroutine(TemporarilyDisableMovementRoutine());
		}

		private IEnumerator ReloadRoutine()
		{
			_isReadyToAttack = false;
			yield return new WaitForSeconds(_reloadSpeed);
			_isReadyToAttack = true;
		}

		private IEnumerator TemporarilyEnableColliderRoutine()
		{
			_weaponCollider.enabled = true;
			yield return new WaitForSeconds(_attackDuration);
			_weaponCollider.enabled = false;
		}

		private IEnumerator TemporarilyDisableMovementRoutine()
		{
			_enemyMovement.AgentSetActive(false);
			yield return new WaitForSeconds(_attackDuration);
			_enemyMovement.AgentSetActive(true);
		}
	}
}