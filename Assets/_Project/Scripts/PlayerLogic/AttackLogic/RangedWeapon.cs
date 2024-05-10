using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class RangedWeapon : WeaponBase
	{
		public override void Attack()
		{
			Debug.Log($"Ranged Attack: {_weaponData.WeaponName} - Damage: {_weaponData.Damage}, Range: {_weaponData.Range}");
		}
	}
}