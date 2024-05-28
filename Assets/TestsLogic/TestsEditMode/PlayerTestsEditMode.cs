using _Project.Scripts.Descriptors;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.Services;
using NUnit.Framework;
using UnityEngine;

namespace TestsLogic.TestsEditMode
{
	public class PlayerTestsEditMode	
	{
		private Player _player;
		private PlayerDescriptor _playerDescriptor;
		private InputService _inputService;

		[SetUp]
		public void Setup()
		{
			_player = new GameObject().AddComponent<Player>();
			_playerDescriptor = ScriptableObject.CreateInstance<PlayerDescriptor>();
			_playerDescriptor.Health = 100;
			_playerDescriptor.RestoreHealthAmount = 10;
			_playerDescriptor.RestoreHealthInterval = 1.0f;
			_inputService = new GameObject().AddComponent<InputService>();
			_player.Init(_playerDescriptor, _inputService);
		}
		
		[TearDown]
		public void Teardown()
		{
			if (_player.gameObject != null)
			{
				Object.DestroyImmediate(_player.gameObject);
			}
			if (_playerDescriptor != null)
			{
				Object.DestroyImmediate(_playerDescriptor);
			}

			if (_inputService.gameObject != null)
			{
				Object.DestroyImmediate(_inputService.gameObject);
			}
		}

		[Test]
		public void PlayerTakesDamage()
		{
			bool healthChanged = false;
			_player.OnPlayerHealthChanged += _ => healthChanged = true;

			_player.TakeDamage(10);
        
			Assert.IsTrue(healthChanged);
			Assert.AreEqual(90, _playerDescriptor.Health - 10);
		}

		[Test]
		public void PlayerDiesWhenHealthIsZero()
		{
			bool isPlayerDestroyed = false;
			_player.OnDestroy += () => isPlayerDestroyed = true;

			_player.TakeDamage(_playerDescriptor.Health);

			Assert.IsTrue(isPlayerDestroyed);
		}
		
		[Test]
		public void PlayerDoesNotTakeNegativeDamage()
		{
			bool healthChanged = false;
			_player.OnPlayerHealthChanged += _ => healthChanged = true;
			_player.TakeDamage(-10);

			Assert.IsFalse(healthChanged);
		}
	}
}
