using System.Collections.Generic;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
    public class PlayerEquipmentController : MonoBehaviour
    {
	    [SerializeField]
	    private Transform _axeSpawnPointTransform;
	    [SerializeField]
	    private Transform _rifleSpawnPointTransform;
	    
        private PlayerAttackController _attackController;
        private WeaponData[] _weaponData;
        private InputService _inputService;
        private readonly List<WeaponBase> _weapons = new();

        private void Start()
        {
            _attackController = GetComponent<PlayerAttackController>();
            _weaponData = GetComponent<Player>().PlayerDescriptor.WeaponDatabase.Weapons;
            _inputService = GetComponent<Player>().InputService;
            
            _inputService.WeaponChangeRequested += EquipWeapon;

            InstantiateAllWeapons();
            EquipWeapon(WeaponType.Melee);
        }

        private void OnDestroy()
        {
	        _inputService.WeaponChangeRequested -= EquipWeapon;
        }

        private void InstantiateAllWeapons()
        {
	        foreach (WeaponData weapon in _weaponData)
	        {
		        if (weapon != null && weapon.WeaponPrefab != null)
		        {
			        Transform spawnPoint;
			        switch (weapon.WeaponType)
			        {
				        case WeaponType.Melee:
					        spawnPoint = _axeSpawnPointTransform;
					        break;
				        case WeaponType.Ranged:
					        spawnPoint = _rifleSpawnPointTransform;
					        break;
				        default:
					        Debug.LogError($"Unknown weapon type: {weapon.WeaponType}");
					        continue;
			        }

			        GameObject weaponObject = Instantiate(weapon.WeaponPrefab, spawnPoint);
			        WeaponBase newWeapon = weaponObject.GetComponent<WeaponBase>();
			        newWeapon.Init(weapon);
			        newWeapon.gameObject.SetActive(false);
			        _weapons.Add(newWeapon);
		        }
	        }
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