using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class RangedWeapon : WeaponBase
	{
		protected override void PerformAttack()
		{
			Debug.Log($"Ranged Attack: {_weaponData.WeaponName} - Damage: {_weaponData.Damage}, Range: {_weaponData.Range}");
			// Здесь можно добавить логику стрельбы, например, создание снаряда или трассировку выстрела
		}
	}
}