using System;
using _Project.Scripts.GameResources;
using _Project.Scripts.PlayerLogic.InventoryLogic;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class PlayerAttackController : MonoBehaviour
	{
		private InputService _inputService;
		private WeaponBase _currentWeapon;
		private InventoryController _inventoryController;
		
		public event Action OnAttacking;
		
		public WeaponType CurrentWeaponType => _currentWeapon.WeaponType;

		private void Start()
		{
			_inventoryController = GetComponent<InventoryController>();
			_inputService = GetComponent<Player>().InputService;
			_inputService.AttackButtonPressed += OnAttackButtonPressed;
		}

		private void OnDestroy()
		{
			_inputService.AttackButtonPressed -= OnAttackButtonPressed;
		}

		public void SetWeapon(WeaponBase weapon)
		{
			_currentWeapon = weapon;
		}

		private void OnAttackButtonPressed()
		{
			if (!_currentWeapon.IsReadyToAttack)
			{
				return;
			}
			if (_currentWeapon == null)
			{
				return;
			}
			
			if (CurrentWeaponType == WeaponType.Ranged && _inventoryController.GetSpecifiedResourceAmountOrLess(ResourceType.Bullet, 1) == 1)
			{
				OnAttacking?.Invoke();
				_currentWeapon.Attack();
			}
			else if (CurrentWeaponType == WeaponType.Melee)
			{
				OnAttacking?.Invoke();
				_currentWeapon.Attack();
			}
		}
	}
}