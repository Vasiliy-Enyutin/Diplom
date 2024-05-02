using System.Collections;
using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Tests.PlayMode.PlayerTestsPlayMode
{
    public class PlayerMovementTests
    {
        private const float MOVE_SPEED = 5f;
        
        private PlayerMovement _playerMovement;
        private Transform _playerGfxTransform;
        private Rigidbody _rigidbody;

        [SetUp]
        public void Setup()
        {
            _playerMovement = new GameObject().AddComponent<PlayerMovement>();
            _rigidbody = _playerMovement.gameObject.AddComponent<Rigidbody>();

            // Загружаем PlayerGFX из ресурсов
            GameObject playerGfxPrefab = Resources.Load<GameObject>("PlayerGFX");

            // Создаем экземпляр PlayerGFX и делаем его дочерним объектом playerObject
            _playerGfxTransform = Object.Instantiate(playerGfxPrefab, _playerMovement.gameObject.transform).transform;
        }
        
        [TearDown]
        public void TearDown()
        {
            // Уничтожаем объект игрока после каждого теста
            if (_playerMovement != null)
            {
                Object.DestroyImmediate(_playerMovement.gameObject);
            }
        }

        [Test]
        public void Move_ChangesMoveDirectionAccordingToPlayerInput()
        {
            // Arrange
            Vector3 moveDirection  = new(0.5f, 0, 0.5f);
            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }

            // Act
            _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);

            // Assert
            Assert.AreEqual(moveDirection, _playerMovement.GetMoveDirectionTest());
        }

        [Test]
        public void Move_NormalizesMoveDirectionWhenMagnitudeIsGreaterThanOne()
        {
            // Arrange
            Vector3 moveDirection  = new(1f, 0f, 1f);
            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }

            // Act
            _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);
        
            // Assert
            Assert.AreEqual(moveDirection.normalized, _playerMovement.GetMoveDirectionTest());
        }
        
        [UnityTest]
        public IEnumerator Move_MovesPlayerAccordingToMoveDirection()
        {
            // Arrange
            Vector3 moveDirection = new(1f, 0f, 1f);
            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }
            Vector3 startPlayerPosition = _playerMovement.transform.position;

            // Act
            _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);
            yield return new WaitForFixedUpdate();

            // Assert
            Vector3 expectedPosition = startPlayerPosition + moveDirection.normalized * MOVE_SPEED * Time.fixedDeltaTime;
            Assert.AreEqual(expectedPosition.x, _playerMovement.transform.position.x);
            Assert.AreEqual(expectedPosition.z, _playerMovement.transform.position.z);
        }

        [Test]
        public void RotatePlayer_MoveDirectionNotZero_RotatesPlayerGfxTransform()
        {
            // Arrange
            Vector3 moveDirection = new(1f, 0f, 1f);
            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }

            // Act
            _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);
        
            // Assert
            Quaternion expectedRotation = Quaternion.LookRotation(moveDirection);
            Assert.AreEqual(expectedRotation, _playerGfxTransform.rotation);
        }
        
        [Test]
        public void RotatePlayer_MoveDirectionZero_DoesNotRotatePlayerGfxTransform()
        {
            // Arrange
            Vector3 moveDirection = Vector3.zero;
            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }
        
            // Act
            _playerMovement.ConstructTest(_playerGfxTransform, moveDirection, MOVE_SPEED, _rigidbody);
        
            // Assert
            Quaternion expectedRotation = Quaternion.identity;
            Assert.AreEqual(expectedRotation, _playerGfxTransform.rotation);
        }
    }
}
