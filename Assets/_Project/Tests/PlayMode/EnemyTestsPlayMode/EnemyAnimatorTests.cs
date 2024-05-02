using System.Collections;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Tests.PlayMode.EnemyTestsPlayMode
{
    public class EnemyAnimatorTests
    {
        private EnemyAnimator _enemyAnimator;
        private Animator _animator;

        [SetUp]
        public void Setup()
        {
            // Создаём объект игрока, добавляем компонент PlayerAnimator и получаем ссылку на него
            _enemyAnimator = new GameObject().AddComponent<EnemyAnimator>();

            // Загружаем EnemyGFX из ресурсов
            GameObject enemyGfxPrefab = Resources.Load<GameObject>("EnemyGFX");

            // Создаем экземпляр PlayerGFX и делаем его дочерним объектом playerObject
            GameObject enemyGfxObject = Object.Instantiate(enemyGfxPrefab, _enemyAnimator.gameObject.transform);

            // Получаем компонент Animator из PlayerGFXObject
            _animator = enemyGfxObject.GetComponent<Animator>();
        }

        [TearDown]
        public void TearDown()
        {
            // Уничтожаем объект игрока после каждого теста
            if (_enemyAnimator != null)
            {
                Object.DestroyImmediate(_enemyAnimator.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator UpdateAnimation_MoveDirectionZero_PlayIdleAnimation()
        {
            // Arrange
            Vector3 moveDirection = Vector3.zero;

            // Act
            _enemyAnimator.ConstructTest(_animator, moveDirection);

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
            _enemyAnimator.ConstructTest(_animator, moveDirection);

            // Wait for the next frame
            yield return null;

            // Assert
            Assert.IsTrue(_animator.GetCurrentAnimatorStateInfo(0).IsName("Run"));
        }
    }
}
