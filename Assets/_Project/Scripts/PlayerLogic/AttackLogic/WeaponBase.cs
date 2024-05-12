using _Project.Scripts.Descriptors;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public abstract class WeaponBase : MonoBehaviour
	{
		protected WeaponData _weaponData;
		
		public WeaponType WeaponType => _weaponData.WeaponType;

		public void SetWeaponData(WeaponData weaponData)
		{
			_weaponData = weaponData;
		}

		public abstract void Attack();
	}
}