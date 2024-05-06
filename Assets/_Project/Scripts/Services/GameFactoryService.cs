using System.Collections.Generic;
using _Project.Scripts.Descriptors;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.PlayerLogic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Services
{
	[UsedImplicitly]
	public class GameFactoryService
	{
		[Inject]
		private AssetProviderService _assetProviderService = null!;
		[Inject]
		private PlayerDescriptor _playerDescriptor = null!;
		[Inject]
		private LocationDescriptor _locationDescriptor = null!;
		[Inject]
		private EnemyDescriptor _enemyDescriptor;

		public Player Player { get; private set; }

		public List<Enemy> Enemies { get; } = new();

		public void CreatePlayer()
		{
			Player = _assetProviderService.CreateAsset<Player>(_playerDescriptor.Prefab, _locationDescriptor.InitialPlayerPositionPoint);
		}

		public void CreateEnemies()
		{
			for (int i = 0; i < _enemyDescriptor.EnemiesNumber; i++)
			{
				Enemy enemy = _assetProviderService.CreateAsset<Enemy>(_enemyDescriptor.Enemy, new Vector3(5f, 0f, 7f));
				enemy.Init(Player.gameObject, _enemyDescriptor.MoveSpeed, _enemyDescriptor.PursuitDistance);
				Enemies.Add(enemy);
			}
		}
		
		public void ClearAll()
		{
			Object.Destroy(Player.gameObject);
			Enemies.ForEach(enemy => Object.Destroy(enemy.gameObject));
		}
	}
}
