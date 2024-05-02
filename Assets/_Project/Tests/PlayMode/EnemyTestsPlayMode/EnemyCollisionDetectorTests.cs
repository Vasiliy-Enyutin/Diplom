using System.Collections;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Tests.PlayMode.EnemyTestsPlayMode
{
    public class EnemyCollisionDetectorTests
    {
        private Player _player;
        private SphereCollider _playerCollider;
        private EnemyCollisionDetector _collisionDetector;

        [SetUp]
        public void Setup()
        {
            _playerCollider = new GameObject().AddComponent<SphereCollider>();
            
            _collisionDetector = new GameObject().AddComponent<EnemyCollisionDetector>();
        }

        [TearDown]
        public void TearDown()
        {
            // Уничтожаем _player после каждого теста
            if (_player != null)
            {
                Object.DestroyImmediate(_player);
            }

            // Уничтожаем _collisionDetector после каждого теста
            if (_collisionDetector != null)
            {
                Object.DestroyImmediate(_collisionDetector);
            }
        }

        [UnityTest]
        public IEnumerator OnTriggerEnter_PlayerComponent_CallsPlayerOnDestroy()
        {
            // Arrange
            _player = _playerCollider.gameObject.AddComponent<Player>();

            bool playerDestroyCalled = false;
            _player.OnDestroy += () => playerDestroyCalled = true;

            // Act
            _collisionDetector.ConstructTest(_playerCollider);

            // Assert
            Assert.IsTrue(playerDestroyCalled);
            yield return null;
        }
    }
}