using System.Collections.Generic;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
    public class PlayerEquipmentController : MonoBehaviour
    {
        private PlayerAttackController _attackController;
        private WeaponDatabase _weaponDatabase;
        private InputService _inputService;
        private readonly List<WeaponBase> _weapons = new();

        private void Start()
        {
            _attackController = GetComponent<PlayerAttackController>();
            _weaponDatabase = GetComponent<Player>().PlayerDescriptor.WeaponDatabase;
            _inputService = GetComponent<Player>().InputService;
            
            _inputService.WeaponChangeRequested += EquipWeapon;

            InstantiateAllWeapons();
            EquipWeapon(WeaponType.Melee);
        }

        private void InstantiateAllWeapons()
        {
	        foreach (WeaponData weaponData in _weaponDatabase.Weapons)
	        {
		        if (weaponData != null && weaponData.WeaponPrefab != null)
		        {
			        GameObject weaponObject = Instantiate(weaponData.WeaponPrefab, gameObject.transform);
			        WeaponBase weapon = weaponObject.GetComponent<WeaponBase>();
			        weapon.SetWeaponData(weaponData);
			        weapon.gameObject.SetActive(false);
			        _weapons.Add(weapon);
		        }
	        }
        }

        private void OnDestroy()
        {
            _inputService.WeaponChangeRequested -= EquipWeapon;
        }

        private void EquipWeapon(WeaponType weaponType)
        {
            // Deactivate all weapons
            foreach (WeaponBase weapon in _weapons)
            {
                if (weapon != null)
                {
                    weapon.gameObject.SetActive(false);
                }
            }

            // Activate the requested weapon
            WeaponBase requestedWeapon = _weapons.Find(weapon => weapon.WeaponType == weaponType);
            if (requestedWeapon != null)
            {
                requestedWeapon.gameObject.SetActive(true);
                _attackController.SetWeapon(requestedWeapon);
            }
            else
            {
                Debug.LogError($"Weapon is missing for {weaponType}!");
            }
        }
    }
}