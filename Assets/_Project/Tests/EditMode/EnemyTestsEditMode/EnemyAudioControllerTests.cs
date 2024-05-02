using System;
using System.Linq;
using _Project.Scripts.EnemyLogic;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Tests.EditMode.EnemyTestsEditMode
{
    public class EnemyAudioControllerTests
    {
        private EnemyAudioController _enemyAudioController;
        private AudioSource _audioSource;

        private AudioClip _idleAudioClip;
        private AudioClip[] _attackAudioClips;

        [SetUp]
        public void Setup()
        {
            _enemyAudioController = new GameObject().AddComponent<EnemyAudioController>();
            _audioSource = _enemyAudioController.gameObject.AddComponent<AudioSource>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_enemyAudioController != null)
            {
                Object.DestroyImmediate(_enemyAudioController.gameObject);
            }
        }

        [Test]
        public void StartPursuitSound_AttackAudioClipsNotEmpty_AudioSourcePlaysAttackClip()
        {
            // Arrange
            bool isPursuingPlayer = true;
            bool isPursuitSoundPlaying = false;
            _attackAudioClips = new[]
            {
                AudioClip.Create("AttackClip1", 1, 1, 44100, false),
                AudioClip.Create("AttackClip2", 1, 1, 44100, false)
            };

            // Act
            _enemyAudioController.ConstructTest(isPursuingPlayer, isPursuitSoundPlaying, _audioSource, null, _attackAudioClips);

            // Assert
            Assert.IsTrue(_audioSource.isPlaying);
            Assert.IsTrue(_attackAudioClips.Contains(_enemyAudioController.CurrentAttackSoundTest));
        }
        
        [Test]
        public void StartPursuitSound_AttackAudioClipsEmpty_AudioSourceDoesNotPlayAttackClip()
        {
            // Arrange
            bool isPursuingPlayer = true;
            bool isPursuitSoundPlaying = false;
            _attackAudioClips = Array.Empty<AudioClip>();

            // Act
            _enemyAudioController.ConstructTest(isPursuingPlayer, isPursuitSoundPlaying, _audioSource, null, _attackAudioClips);
        
            // Assert
            Assert.IsNull(_enemyAudioController.CurrentAttackSoundTest);
        }
        
        [Test]
        public void PlayIdleSound_IdleAudioClipNotEmpty_IdleAudioClipPlayed()
        {
            // Arrange
            bool isPursuingPlayer = false;
            bool isPursuitSoundPlaying = true;
            _idleAudioClip = AudioClip.Create("IdleClip", 1, 1, 44100, false);
        
            // Act
            _enemyAudioController.ConstructTest(isPursuingPlayer, isPursuitSoundPlaying, _audioSource, _idleAudioClip);
        
            // Assert
            Assert.IsTrue(_audioSource.isPlaying);
            Assert.AreEqual(_idleAudioClip, _audioSource.clip);
        }

        [Test]
        public void PlayIdleSound_IdleAudioClipEmpty_AudioSourceDoesNotPlayIdleClip()
        {
            // Arrange
            bool isPursuingPlayer = false;
            bool isPursuitSoundPlaying = true;
            _idleAudioClip = null;
        
            // Act
            _enemyAudioController.ConstructTest(isPursuingPlayer, isPursuitSoundPlaying, _audioSource, _idleAudioClip);
        
            // Assert
            Assert.IsFalse(_audioSource.isPlaying);
        }
    }
}
