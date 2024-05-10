using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class RangedWeapon : WeaponBase
	{
		public override void Attack()
		{
			Debug.Log($"Ranged Attack: {_weaponData.weaponName} - Damage: {_weaponData.damage}, Range: {_weaponData.range}");
		}
	}
}