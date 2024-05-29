using _Project.Scripts.Descriptors;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.PlayerLogic.AttackLogic;
using _Project.Scripts.PlayerLogic.InventoryLogic;
using _Project.Scripts.Services;
using _Project.Scripts.GameResources;
using System.Collections;

namespace TestsLogic.TestsPlayMode
{
    public class PlayerAttackControllerTestsPlayMode
    {
        private GameObject _playerObject;
        private PlayerAttackController _playerAttackController;
        private InputService _inputService;
        private InventoryController _inventoryController;
        private TestWeapon _weapon;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            _playerObject = new GameObject();
            var player = _playerObject.AddComponent<Player>();
            _inventoryController = _playerObject.AddComponent<InventoryController>();
            _playerAttackController = _playerObject.AddComponent<PlayerAttackController>();
            _inputService = _playerObject.AddComponent<InputService>();

            // Инициализируем игрока
            player.Init(new PlayerDescriptor(), _inputService);

            // Создаем оружие и добавляем его как компонент к новому игровому объекту
            var weaponObject = new GameObject();
            _weapon = weaponObject.AddComponent<TestWeapon>();

            // Ждем до конца кадра, чтобы все компоненты инициализировались
            yield return null;
        }

        [UnityTearDown]
        public IEnumerator Teardown()
        {
            Object.DestroyImmediate(_playerObject);
            if (_weapon != null)
            {
                Object.DestroyImmediate(_weapon.gameObject);
            }
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerCanAttackWithMeleeWeapon()
        {
            _weapon.Init(new WeaponData { WeaponType = WeaponType.Melee, ReloadSpeed = 1f });
            _playerAttackController.SetWeapon(_weapon);

            bool attackInvoked = false;
            _playerAttackController.OnAttacking += () => attackInvoked = true;

            _playerAttackController.OnAttackButtonPressed();

            yield return null;

            Assert.IsTrue(attackInvoked);
            Assert.IsFalse(_weapon.IsReadyToAttack, "Weapon should not be ready immediately after attack");
        }

        [UnityTest]
        public IEnumerator PlayerCanAttackWithRangedWeapon()
        {
            _weapon.Init(new WeaponData { WeaponType = WeaponType.Ranged, ReloadSpeed = 1f });
            _inventoryController.AddResource(ResourceType.Bullet, 10);
            _playerAttackController.SetWeapon(_weapon);

            bool attackInvoked = false;
            _playerAttackController.OnAttacking += () => attackInvoked = true;

            _playerAttackController.OnAttackButtonPressed();

            yield return null;

            Assert.IsTrue(attackInvoked);
            Assert.IsFalse(_weapon.IsReadyToAttack, "Weapon should not be ready immediately after attack");
        }

        [UnityTest]
        public IEnumerator PlayerCannotAttackWithRangedWeaponWhenOutOfAmmo()
        {
            _weapon.Init(new WeaponData { WeaponType = WeaponType.Ranged, ReloadSpeed = 1f });
            _inventoryController.AddResource(ResourceType.Bullet, 0);
            _playerAttackController.SetWeapon(_weapon);

            bool attackInvoked = false;
            _playerAttackController.OnAttacking += () => attackInvoked = true;

            _playerAttackController.OnAttackButtonPressed();

            yield return null;

            Assert.IsFalse(attackInvoked, "Attack should not be invoked without ammo");
        }
        
        [UnityTest]
        public IEnumerator PlayerCannotAttackWithRangedWeaponWhenAmmoLessThanZero()
        {
	        _weapon.Init(new WeaponData { WeaponType = WeaponType.Ranged, ReloadSpeed = 1f });
	        _inventoryController.AddResource(ResourceType.Bullet, -10);
	        _playerAttackController.SetWeapon(_weapon);

	        bool attackInvoked = false;
	        _playerAttackController.OnAttacking += () => attackInvoked = true;

	        _playerAttackController.OnAttackButtonPressed();

	        yield return null;

	        Assert.IsFalse(attackInvoked, "Attack should not be invoked without ammo");
        }

        private class TestWeapon : WeaponBase
        {
            protected override void PerformAttack()
            {
                // Mock attack logic
            }
        }
    }
}
