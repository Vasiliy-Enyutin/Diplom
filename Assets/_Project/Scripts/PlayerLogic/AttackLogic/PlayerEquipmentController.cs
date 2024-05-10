using _Project.Scripts.Descriptors;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class PlayerEquipmentController : MonoBehaviour
	{
		private PlayerAttackController _attackController;
		private WeaponDatabase _weaponDatabase;

		private void Start()
		{
			_attackController = GetComponent<PlayerAttackController>();
			_weaponDatabase = GetComponent<Player>().PlayerDescriptor.WeaponDatabase;
			EquipWeapon(WeaponType.Melee);
		}

		public void EquipWeapon(WeaponType weaponType)
		{
			Debug.Log(_weaponDatabase);
			WeaponData weaponData = _weaponDatabase.GetWeaponData(weaponType);
			if (weaponData != null && weaponData.weaponPrefab != null)
			{
				GameObject weaponObject = Instantiate(weaponData.weaponPrefab);
				WeaponBase weapon = weaponObject.GetComponent<WeaponBase>();
				weapon.SetWeaponData(weaponData);
				_attackController.SetWeapon(weapon);
			}
			else
			{
				Debug.LogError("Weapon data or weapon prefab is missing!");
			}
		}
	}
}