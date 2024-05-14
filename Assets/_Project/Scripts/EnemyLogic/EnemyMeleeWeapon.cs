using System.Collections;
using UnityEngine;

namespace _Project.Scripts.EnemyLogic
{
	public class EnemyMeleeWeapon : MonoBehaviour
	{
		// [SerializeField]
		// private SphereCollider _weaponCollider;
		//
		// private void Awake()
		// {
		// 	_weaponCollider.enabled = false;
		// }
		//
		// private void PerformAttack()
		// {
		// 	StartCoroutine(TemporarilyEnableCollider());
		// 	Debug.Log($"Melee Attack: {_weaponData.WeaponName} - Damage: {_weaponData.Damage}");
		// }
		//
		// private IEnumerator TemporarilyEnableCollider()
		// {
		// 	_weaponCollider.enabled = true;
		// 	yield return new WaitForSeconds(_weaponData.AttackDuration);
		// 	_weaponCollider.enabled = false;
		// }
		//
		// private void OnTriggerEnter(Collider other)
		// {
		// 	if (other.TryGetComponent(out Enemy enemy))
		// 	{
		// 		if (_damagedEnemies.Contains(enemy))
		// 		{
		// 			return;
		// 		}
		//
		// 		_damagedEnemies.Add(enemy);
		// 		enemy.TakeDamage(_weaponData.Damage);
		// 	}
		// }
	}
}