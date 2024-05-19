using System.Collections.Generic;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Descriptors.GameResources;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.GameResources;
using _Project.Scripts.PlayerLogic;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
	public class GameFactoryService
	{
		private readonly AssetProviderService _assetProviderService;
		private readonly PlayerDescriptor _playerDescriptor;
		private readonly CameraDescriptor _cameraDescriptor;
		private readonly LocationDescriptor _locationDescriptor;
		private readonly ResourcesDatabase _resourcesDatabase;
		private readonly MainBuildingDescriptor _mainBuildingDescriptor;
		private readonly EnemyDescriptor _enemyDescriptor;
		private readonly InputService _inputService;

		public Player Player { get; private set; }

		public List<Enemy> Enemies { get; } = new();

		public MainBuilding MainBuilding;
		private readonly List<GameResource> _gameResources = new();

		[Inject]
		private GameFactoryService(
			AssetProviderService assetProviderService,
			PlayerDescriptor playerDescriptor,
			CameraDescriptor cameraDescriptor,
			LocationDescriptor locationDescriptor,
			ResourcesDatabase resourcesDatabase,
			MainBuildingDescriptor mainBuildingDescriptor,
			EnemyDescriptor enemyDescriptor,
			InputService inputService)
		{
			_assetProviderService = assetProviderService;
			_playerDescriptor = playerDescriptor;
			_cameraDescriptor = cameraDescriptor;
			_locationDescriptor = locationDescriptor;
			_resourcesDatabase = resourcesDatabase;
			_mainBuildingDescriptor = mainBuildingDescriptor;
			_enemyDescriptor = enemyDescriptor;
			_inputService = inputService;
		}
		
		public void CreateMainBuilding()
		{
			MainBuilding = _assetProviderService.CreateAsset<MainBuilding>(_mainBuildingDescriptor.Prefab,
				_locationDescriptor.InitialMainBuildingPositionPoint);
			MainBuilding.Init(_mainBuildingDescriptor);
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
		
		public void CreateResources()
		{
			List<Vector3> availableSpawnPoints = new(_locationDescriptor.InitialResourcesSpawnPoints);

			foreach (ResourceDescriptor resourceDescriptor in _resourcesDatabase.Resources)
			{
				int resourcesNumberOnMap = resourceDescriptor.ResourcesNumberOnMap;

				for (int i = 0; i < resourcesNumberOnMap; i++)
				{
					if (availableSpawnPoints.Count > 0)
					{
						int randomIndex = Random.Range(0, availableSpawnPoints.Count);
						Vector3 spawnPoint = availableSpawnPoints[randomIndex];

						GameResource resource = _assetProviderService.CreateAsset<GameResource>(resourceDescriptor.ResourcePrefab, spawnPoint);
						resource.Init(resourceDescriptor.ResourceType, resourceDescriptor.ResourcesAmount);
						resource.OnGameResourceCollected += HandleResourceCollected;
						_gameResources.Add(resource);

						availableSpawnPoints.RemoveAt(randomIndex);
					}
					else
					{
						break;
					}
				}
			}
		}

		public void CreateEnemies()
		{
			List<Vector3> availableSpawnPoints = new(_locationDescriptor.InitialEnemyPositionPoints);

			for (int i = 0; i < _enemyDescriptor.EnemiesNumber; i++)
			{
				if (availableSpawnPoints.Count > 0)
				{
					int randomIndex = Random.Range(0, availableSpawnPoints.Count);
					Vector3 spawnPoint = availableSpawnPoints[randomIndex];

					Enemy enemy = _assetProviderService.CreateAsset<Enemy>(_enemyDescriptor.Enemy, spawnPoint);
					enemy.Init(Player.gameObject, _enemyDescriptor, MainBuilding);
					enemy.OnEnemyDied += HandleEnemyDied;
					Enemies.Add(enemy);

					availableSpawnPoints.RemoveAt(randomIndex);
				}
				else
				{
					break;
				}
			}
		}

		public void DestroyAllResources()
		{
			int resourcesCount = _gameResources.Count;
			for (int i = 0; i < resourcesCount; i++)
			{
				_gameResources[0].Collect();
			}
		}

		public void DestroyAllEnemies()
		{
			int enemiesCount = Enemies.Count;
			for (int i = 0; i < enemiesCount; i++)
			{
				Enemies[0].TakeDamage(_enemyDescriptor.Health);
			}
		}

		private void HandleResourceCollected(GameResource resource)
		{
			resource.OnGameResourceCollected -= HandleResourceCollected;
			_gameResources.Remove(resource);
		}

		private void HandleEnemyDied(Enemy enemy)
		{
			enemy.OnEnemyDied -= HandleEnemyDied;
			Enemies.Remove(enemy);
		}
	}
}
