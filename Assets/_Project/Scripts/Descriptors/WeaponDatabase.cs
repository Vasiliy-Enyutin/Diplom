using _Project.Scripts.PlayerLogic.AttackLogic;
using UnityEngine;

namespace _Project.Scripts.Descriptors
{
	[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Weapons/WeaponDatabase")]
	public class WeaponDatabase : ScriptableObject
	{
		public WeaponData meleeWeapon;
		public WeaponData rangedWeapon;

		public WeaponData GetWeaponData(WeaponType weaponType)
		{
			switch (weaponType)
			{
				case WeaponType.Melee:
					return meleeWeapon;
				case WeaponType.Ranged:
					return rangedWeapon;
				default:
					return null;
			}
		}
	}
}