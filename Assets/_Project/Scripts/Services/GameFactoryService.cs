using System.Collections.Generic;
using _Project.Scripts.Descriptors;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.PlayerLogic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

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
		private CameraDescriptor _cameraDescriptor = null!;
		[Inject]
		private LocationDescriptor _locationDescriptor = null!;
		[Inject]
		private MainBuildingDescriptor _mainBuildingDescriptor = null!;
		[Inject]
		private EnemyDescriptor _enemyDescriptor;
		[Inject]
		private InputService _inputService;

		public Player Player { get; private set; }

		public List<Enemy> Enemies { get; } = new();

		private MainBuilding _mainBuilding;

		public void CreateMainBuilding()
		{
			_mainBuilding = _assetProviderService.CreateAsset<MainBuilding>(_mainBuildingDescriptor.Prefab,
				_locationDescriptor.InitialMainBuildingPositionPoint);
			_mainBuilding.Init(_mainBuildingDescriptor);
		}

		public void CreatePlayer()
		{
			Player = _assetProviderService.CreateAsset<Player>(_playerDescriptor.Prefab, _locationDescriptor.InitialPlayerPositionPoint);
			Player.Init(_playerDescriptor, _inputService);
		}

		public void CreateCamera()
		{
			CinemachineVirtualCamera virtualCamera =
				_assetProviderService.CreateAsset<CinemachineVirtualCamera>(_cameraDescriptor.CameraPrefab,
					_locationDescriptor.InitialPlayerPositionPoint);
			
			virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = _cameraDescriptor.CameraOffset;
			virtualCamera.transform.rotation = _cameraDescriptor.CameraRotation;
			Transform target = Player.transform;
			virtualCamera.Follow = target;
			virtualCamera.LookAt = target;
		}

		public void CreateEnemies()
		{
			for (int i = 0; i < _enemyDescriptor.EnemiesNumber; i++)
			{
				Enemy enemy = _assetProviderService.CreateAsset<Enemy>(_enemyDescriptor.Enemy, _locationDescriptor.InitialEnemyPositionPoint);
				enemy.Init(Player.gameObject, _enemyDescriptor, _mainBuilding);
				enemy.OnEnemyDied += HandleEnemyDied;
				Enemies.Add(enemy);
			}
		}

		public void DestroyAllEnemies()
		{
			for (int i = 0; i < Enemies.Count; i++)
			{
				Enemies[0].TakeDamage(_enemyDescriptor.Health);
			}
		}

		private void HandleEnemyDied(Enemy enemy)
		{
			Enemies.Remove(enemy);
		}
	}
}
