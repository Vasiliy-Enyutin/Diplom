using System.Collections;
using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Tests.PlayMode.PlayerTestsPlayMode
{
    public class PlayerTests
    {
        private Player _player;
        private bool _onReachedFinishCalled;
        private bool _onDestroyCalled;

        [SetUp]
        public void Setup()
        {
            // Создаем новый объект Player для каждого теста
            _player = new GameObject().AddComponent<Player>();
            _onReachedFinishCalled = false;
            _onDestroyCalled = false;
        }

        [TearDown]
        public void TearDown()
        {
            // Уничтожаем объект Player после каждого теста
            if (_player != null)
            {
                Object.Destroy(_player.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator Die_InvokeOnDestroyEvent_DestroyGameObject()
        {
            // Подписываемся на событие OnDestroy
            _player.OnDestroy += () => _onDestroyCalled = true;

            // Act
            _player.Die();

            // Ждем один кадр, чтобы позволить Unity обработать события и уничтожить объект.
            yield return null;

            // Assert
            Assert.IsTrue(_onDestroyCalled);
            Assert.IsTrue(_player == null || _player.gameObject == null);
        }

        [Test]
        public void CollisionWithFinish_InvokeOnReachedFinishEvent()
        {
            // Подписываемся на событие OnReachedFinish
            _player.OnReachedFinish += () => _onReachedFinishCalled = true;

            // Act
            _player.CollisionWithFinish();

            // Assert
            Assert.IsTrue(_onReachedFinishCalled);
        }
    }
}