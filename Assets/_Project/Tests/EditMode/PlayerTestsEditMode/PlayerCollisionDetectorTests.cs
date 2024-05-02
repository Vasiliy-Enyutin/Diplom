using _Project.Scripts;
using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;

namespace _Project.Tests.EditMode.PlayerTestsEditMode
{
    public class PlayerCollisionDetectorTests
    {
        private SphereCollider _finishCollider;
        private Player _player;
        private PlayerCollisionDetector _collisionDetector;

        [SetUp]
        public void Setup()
        {
            _finishCollider = new GameObject().AddComponent<SphereCollider>();
            
            _player = new GameObject().AddComponent<Player>();
            _collisionDetector = _player.gameObject.AddComponent<PlayerCollisionDetector>();
        }

        [TearDown]
        public void TearDown()
        {
            // Уничтожаем _player после каждого теста
            if (_player != null)
            {
                Object.DestroyImmediate(_player);
            }

            // Уничтожаем _finishCollider после каждого теста
            if (_finishCollider != null)
            {
                Object.DestroyImmediate(_finishCollider);
            }
        }

        [Test]
        public void OnTriggerEnter_FinishCollider_CallsCollisionWithFinish()
        {
            // Arrange
            _finishCollider.gameObject.AddComponent<Finish>();

            bool collisionWithFinishCalled = false;
            _player.OnReachedFinish += () => collisionWithFinishCalled = true;

            // Act
            _collisionDetector.ConstructTest(_finishCollider, _player);

            // Assert
            Assert.IsTrue(collisionWithFinishCalled);
        }

        [Test]
        public void OnTriggerEnter_OtherCollider_DoesNotCallCollisionWithFinish()
        {
            // Arrange
            bool collisionWithFinishCalled = false;
            _player.OnReachedFinish += () => collisionWithFinishCalled = true;

            // Act
            _collisionDetector.ConstructTest(_finishCollider, _player);

            // Assert
            Assert.IsFalse(collisionWithFinishCalled);
        }
    }
}