using System.Collections;
using _Project.Scripts.Descriptors;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public abstract class WeaponBase : MonoBehaviour
	{
		protected WeaponData _weaponData;
		private bool _isReadyToAttack = true;

		public WeaponType WeaponType => _weaponData.WeaponType;
		public bool IsReadyToAttack => _isReadyToAttack;

		public void Init(WeaponData weaponData)
		{
			_weaponData = weaponData;
		}

		public void Attack()
		{
			if (_isReadyToAttack)
			{
				PerformAttack();
				StartCoroutine(ReloadWeapon());
			}
		}

		protected abstract void PerformAttack();

		private IEnumerator ReloadWeapon()
		{
			_isReadyToAttack = false;
			yield return new WaitForSeconds(_weaponData.ReloadSpeed);
			_isReadyToAttack = true;
		}
	}
}