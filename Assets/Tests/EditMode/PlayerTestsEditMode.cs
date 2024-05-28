using _Project.Scripts.Descriptors;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.Services;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
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
			_playerDescriptor = new PlayerDescriptor { Health = 100, RestoreHealthAmount = 10, RestoreHealthInterval = 1.0f };
			_inputService = new InputService();
			_player.Init(_playerDescriptor, _inputService);
		}

		[Test]
		public void PlayerTakesDamage()
		{
			bool healthChanged = false;
			_player.OnPlayerHealthChanged += _ => healthChanged = true;

			_player.TakeDamage(10);
        
			Assert.IsTrue(healthChanged);
		}

		[Test]
		public void PlayerDiesWhenHealthIsZero()
		{
			bool isPlayerDestroyed = false;
			_player.OnDestroy += () => isPlayerDestroyed = true;

			_player.TakeDamage(_playerDescriptor.Health);

			Assert.IsTrue(isPlayerDestroyed);
		}
	}
}
