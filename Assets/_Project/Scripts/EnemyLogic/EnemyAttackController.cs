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

		private EnemyMovement _enemyMovement;

		private float _damage;
		private float _attackDuration;
		private float _reloadSpeed;
		private bool _isReadyToAttack = true;

		private void Start()
		{
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
				// TODO attack anim
				PerformAttack();
			}
		}

		private void PerformAttack()
		{
			Debug.Log("Attack enemy");
			StartCoroutine(ReloadRoutine());
			StartCoroutine(TemporarilyEnableColliderRoutine());
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
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out Player player))
			{
				player.TakeDamage(_damage);
			}
		}
		// private WeaponBase _currentWeapon;
		//
		// public event Action OnAttacking;
		//
		// public WeaponType CurrentWeaponType => _currentWeapon.WeaponType;
		//
		// private void Start()
		// {
		// 	_inputService = GetComponent<Player>().InputService;
		// 	_inputService.AttackButtonPressed += OnAttackButtonPressed;
		// }
		//
		// private void OnDestroy()
		// {
		// 	_inputService.AttackButtonPressed -= OnAttackButtonPressed;
		// }
		//
		// public void SetWeapon(WeaponBase weapon)
		// {
		// 	_currentWeapon = weapon;
		// }

		// private void OnAttackButtonPressed()
		// { 
		// 	if (_currentWeapon.IsReadyToAttack)
		// 	{
		// 		if (_currentWeapon != null)
		// 		{
		// 			OnAttacking?.Invoke();
		// 			_currentWeapon.Attack();
		// 		}
		// 	}
		// }
	}
}