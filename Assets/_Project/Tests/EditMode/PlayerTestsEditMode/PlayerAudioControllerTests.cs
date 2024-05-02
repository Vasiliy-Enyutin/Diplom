using _Project.Scripts.PlayerLogic;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Tests.EditMode.PlayerTestsEditMode
{
    public class PlayerAudioControllerTests
    {
        private PlayerAudioController _playerAudioController;
        private AudioSource _audioSource;
        private readonly AudioClip[] _audioClips = new AudioClip[2];

        [SetUp]
        public void Setup()
        {
            // Создаём объект игрока и добавляем компонент PlayerAudioController и получаем ссылку на него
            _playerAudioController = new GameObject().AddComponent<PlayerAudioController>();

            // Добавляем компонент AudioSource
            _audioSource = _playerAudioController.gameObject.AddComponent<AudioSource>();
        }

        [TearDown]
        public void TearDown()
        {
            // Уничтожаем объект Player после каждого теста
            if (_playerAudioController != null)
            {
                Object.DestroyImmediate(_playerAudioController.gameObject);
            }
        }

        [Test]
        public void ConstructTest_MoveDirectionZero_NoAudioClipPlayed()
        {
            // Arrange
            Vector3 moveDirection = Vector3.zero;
            _audioClips[0] = AudioClip.Create("TestClip1", 1, 1, 44100, false);
            _audioClips[1] = AudioClip.Create("TestClip2", 1, 1, 44100, false);

            // Act
            _playerAudioController.ConstructTest(moveDirection, _audioSource, _audioClips);

            // Assert
            Assert.IsFalse(_audioSource.isPlaying);
        }

        [Test]
        public void ConstructTest_MoveDirectionNotZero_AudioClipPlayed()
        {
            // Arrange
            Vector3 moveDirection = Vector3.forward;
            _audioClips[0] = AudioClip.Create("TestClip1", 1, 1, 44100, false);
            _audioClips[1] = AudioClip.Create("TestClip2", 1, 1, 44100, false);

            // Act
            _playerAudioController.ConstructTest(moveDirection, _audioSource, _audioClips);

            // Assert
            Assert.IsTrue(_audioSource.isPlaying);
        }

        [Test]
        public void ConstructTest_ClearAudioClips_NoAudioClipPlayed()
        {
            // Arrange
            Vector3 moveDirection = Vector3.forward;

            // Act
            _playerAudioController.ConstructTest(moveDirection, _audioSource, _audioClips);

            // Assert
            Assert.IsFalse(_audioSource.isPlaying);
        }
    }
}
