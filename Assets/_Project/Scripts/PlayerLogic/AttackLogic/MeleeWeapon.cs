using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class MeleeWeapon : WeaponBase
	{
		public override void Attack()
		{
			Debug.Log($"Melee Attack: {_weaponData.WeaponName} - Damage: {_weaponData.Damage}");
		}
	}
}