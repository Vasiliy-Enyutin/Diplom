using _Project.Scripts;
using NUnit.Framework;
using UnityEngine;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.Descriptors;

namespace TestsLogic.TestsEditMode
{
    public class EnemyTestsEditMode
    {
        private GameObject _enemyObject;
        private Enemy _enemy;
        private GameObject _playerObject;
        private MainBuilding _mainBuilding;
        private EnemyDescriptor _enemyDescriptor;

        [SetUp]
        public void Setup()
        {
            // Создание объекта игрока
            _playerObject = new GameObject();
            _playerObject.name = "Player";

            // Создание главного здания
            var mainBuildingObject = new GameObject();
            _mainBuilding = mainBuildingObject.AddComponent<MainBuilding>();

            // Создание объекта врага и добавление необходимых компонентов
            _enemyObject = new GameObject();
            _enemy = _enemyObject.AddComponent<Enemy>();

            // Инициализация врага
            _enemyDescriptor = ScriptableObject.CreateInstance<EnemyDescriptor>();
            _enemyDescriptor.Health = 100;
            _enemy.Init(_playerObject, _enemyDescriptor, _mainBuilding);
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_enemyObject);
            Object.DestroyImmediate(_playerObject);
            Object.DestroyImmediate(_mainBuilding.gameObject);
        }

        [Test]
        public void EnemyDies()
        {
            bool enemyDied = false;
            _enemy.OnEnemyDied += (enemy) => enemyDied = true;

            _enemy.TakeDamage(_enemyDescriptor.Health);
            Assert.IsTrue(enemyDied);
        }

        [Test]
        public void EnemyInitializationSetsProperties()
        {
            Assert.AreEqual(_playerObject, _enemy.Target);
            Assert.AreEqual(_mainBuilding, _enemy.MainBuilding);
            Assert.AreEqual(_enemyDescriptor, _enemy.EnemyDescriptor);
        }
    }
}
