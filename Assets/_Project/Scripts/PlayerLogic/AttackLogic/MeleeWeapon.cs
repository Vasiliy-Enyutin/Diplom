using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.EnemyLogic;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class MeleeWeapon : WeaponBase
	{
		private SphereCollider _weaponCollider;
		private HashSet<Enemy> _damagedEnemies;

		private void Awake()
		{
			_weaponCollider = GetComponent<SphereCollider>();
			_weaponCollider.enabled = false;
			_damagedEnemies = new HashSet<Enemy>();
		}

		protected override void PerformAttack()
		{
			_damagedEnemies.Clear();
			StartCoroutine(TemporarilyEnableCollider());
			Debug.Log($"Melee Attack: {_weaponData.WeaponName} - Damage: {_weaponData.Damage}");
		}

		private IEnumerator TemporarilyEnableCollider()
		{
			_weaponCollider.enabled = true;
			yield return new WaitForSeconds(_weaponData.AttackDuration);
			_weaponCollider.enabled = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out Enemy enemy))
			{
				if (_damagedEnemies.Contains(enemy))
				{
					return;
				}

				_damagedEnemies.Add(enemy);
				enemy.TakeDamage(_weaponData.Damage);
			}
		}
	}
}