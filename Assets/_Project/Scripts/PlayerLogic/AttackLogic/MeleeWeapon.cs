using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class MeleeWeapon : WeaponBase
	{
		public override void Attack()
		{
			Debug.Log($"Melee Attack: {_weaponData.weaponName} - Damage: {_weaponData.damage}");
		}
	}
}