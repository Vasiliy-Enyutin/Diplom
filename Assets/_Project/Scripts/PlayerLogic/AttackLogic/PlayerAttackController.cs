using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.AttackLogic
{
	public class PlayerAttackController : MonoBehaviour
	{
		private InputService _inputService;
		private WeaponBase _currentWeapon;

		public WeaponType CurrentWeaponType => _currentWeapon.WeaponType;

		private void Start()
		{
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
			if (_currentWeapon != null)
			{
				_currentWeapon.Attack();
			}
		}
	}
}