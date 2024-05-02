using System.Collections;
using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Tests.PlayMode.PlayerTestsPlayMode
{
    public class PlayerAnimatorTests
    {
        private PlayerAnimator _playerAnimator;
        private Animator _animator;

        [SetUp]
        public void Setup()
        {
            // Создаём объект игрока, добавляем компонент PlayerAnimator и получаем ссылку на него
            _playerAnimator = new GameObject().AddComponent<PlayerAnimator>();

            // Загружаем PlayerGFX из ресурсов
            GameObject playerGfxPrefab = Resources.Load<GameObject>("PlayerGFX");

            // Создаем экземпляр PlayerGFX и делаем его дочерним объектом playerObject
            GameObject playerGfxObject = Object.Instantiate(playerGfxPrefab, _playerAnimator.gameObject.transform);

            // Получаем компонент Animator из PlayerGFXObject
            _animator = playerGfxObject.GetComponent<Animator>();
        }

        [TearDown]
        public void TearDown()
        {
            // Уничтожаем объект игрока после каждого теста
            if (_playerAnimator != null)
            {
                Object.DestroyImmediate(_playerAnimator.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator UpdateAnimation_MoveDirectionZero_PlayIdleAnimation()
        {
            // Arrange
            Vector3 moveDirection = Vector3.zero;

            // Act
            _playerAnimator.ConstructTest(_animator, moveDirection);

            // Wait for the next frame
            yield return null;

            // Assert
            Assert.IsTrue(_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        }

        [UnityTest]
        public IEnumerator UpdateAnimation_MoveDirectionNotZero_PlayRunAnimation()
        {
            // Arrange
            Vector3 moveDirection = Vector3.forward;

            // Act
            _playerAnimator.ConstructTest(_animator, moveDirection);

            // Wait for the next frame
            yield return null;

            // Assert
            Assert.IsTrue(_animator.GetCurrentAnimatorStateInfo(0).IsName("Run"));
        }
    }
}
