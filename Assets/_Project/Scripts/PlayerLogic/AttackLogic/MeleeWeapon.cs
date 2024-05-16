using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.EnemyLogic;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class MeleeWeapon : WeaponBase
	{
		private SphereCollider _weaponCollider;
		private HashSet<IDamageable> _damagedEnemies;

		private void Awake()
		{
			_weaponCollider = GetComponent<SphereCollider>();
			_weaponCollider.enabled = false;
			_damagedEnemies = new HashSet<IDamageable>();
		}

		protected override void PerformAttack()
		{
			_damagedEnemies.Clear();
			StartCoroutine(TemporarilyEnableCollider());
		}

		private IEnumerator TemporarilyEnableCollider()
		{
			_weaponCollider.enabled = true;
			yield return new WaitForSeconds(_weaponData.AttackDuration);
			_weaponCollider.enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IDamageable damageable))
			{
				if (_damagedEnemies.Contains(damageable))
				{
					return;
				}

				_damagedEnemies.Add(damageable);
				damageable.TakeDamage(_weaponData.Damage);
			}
		}
	}
}