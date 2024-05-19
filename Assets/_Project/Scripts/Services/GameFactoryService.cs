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
		private readonly ObjectsLocatorService _objectsLocatorService;
		
		[Inject]
		private GameFactoryService(
			AssetProviderService assetProviderService,
			PlayerDescriptor playerDescriptor,
			CameraDescriptor cameraDescriptor,
			LocationDescriptor locationDescriptor,
			ResourcesDatabase resourcesDatabase,
			MainBuildingDescriptor mainBuildingDescriptor,
			EnemyDescriptor enemyDescriptor,
			InputService inputService,
			ObjectsLocatorService objectsLocatorService)
		{
			_assetProviderService = assetProviderService;
			_playerDescriptor = playerDescriptor;
			_cameraDescriptor = cameraDescriptor;
			_locationDescriptor = locationDescriptor;
			_resourcesDatabase = resourcesDatabase;
			_mainBuildingDescriptor = mainBuildingDescriptor;
			_enemyDescriptor = enemyDescriptor;
			_inputService = inputService;
			_objectsLocatorService = objectsLocatorService;
		}
		
		public void CreateMainBuilding()
		{
			MainBuilding mainBuilding = _assetProviderService.CreateAsset<MainBuilding>(_mainBuildingDescriptor.Prefab,
				_locationDescriptor.InitialMainBuildingPositionPoint);
			mainBuilding.Init(_mainBuildingDescriptor);
			_objectsLocatorService.MainBuilding = mainBuilding;
		}

		public void CreatePlayer()
		{
			Player player = _assetProviderService.CreateAsset<Player>(_playerDescriptor.Prefab, _locationDescriptor.InitialPlayerPositionPoint);
			player.Init(_playerDescriptor, _inputService);
			_objectsLocatorService.Player = player;
		}

		public void CreateCamera()
		{
			CinemachineVirtualCamera virtualCamera =
				_assetProviderService.CreateAsset<CinemachineVirtualCamera>(_cameraDescriptor.CameraPrefab,
					_locationDescriptor.InitialPlayerPositionPoint);
			
			virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = _cameraDescriptor.CameraOffset;
			virtualCamera.transform.rotation = _cameraDescriptor.CameraRotation;
			Transform target = _objectsLocatorService.Player.transform;
			virtualCamera.Follow = target;
			virtualCamera.LookAt = target;
		}
		
		public void CreateResources()
		{
			List<Vector3> availableSpawnPoints = new(_locationDescriptor.InitialResourcesSpawnPoints);
			List<GameResource> gameResources = new();

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
						gameResources.Add(resource);

						availableSpawnPoints.RemoveAt(randomIndex);
					}
					else
					{
						break;
					}
				}
			}

			_objectsLocatorService.GameResources = gameResources;
		}

		public void CreateEnemies()
		{
			List<Vector3> availableSpawnPoints = new(_locationDescriptor.InitialEnemyPositionPoints);
			List<Enemy> enemies = new();

			for (int i = 0; i < _enemyDescriptor.EnemiesNumber; i++)
			{
				if (availableSpawnPoints.Count > 0)
				{
					int randomIndex = Random.Range(0, availableSpawnPoints.Count);
					Vector3 spawnPoint = availableSpawnPoints[randomIndex];

					Enemy enemy = _assetProviderService.CreateAsset<Enemy>(_enemyDescriptor.Enemy, spawnPoint);
					enemy.Init(_objectsLocatorService.Player.gameObject, _enemyDescriptor, _objectsLocatorService.MainBuilding);
					enemy.OnEnemyDied += HandleEnemyDied;
					enemies.Add(enemy);

					availableSpawnPoints.RemoveAt(randomIndex);
				}
				else
				{
					break;
				}
			}

			_objectsLocatorService.Enemies = enemies;
		}

		public void DestroyAllResources()
		{
			int resourcesCount = _objectsLocatorService.GameResources.Count;
			for (int i = 0; i < resourcesCount; i++)
			{
				_objectsLocatorService.GameResources[0].Collect();
			}
		}

		public void DestroyAllEnemies()
		{
			int enemiesCount = _objectsLocatorService.Enemies.Count;
			for (int i = 0; i < enemiesCount; i++)
			{
				_objectsLocatorService.Enemies[0].TakeDamage(_enemyDescriptor.Health);
			}
		}

		private void HandleResourceCollected(GameResource resource)
		{
			resource.OnGameResourceCollected -= HandleResourceCollected;
			_objectsLocatorService.GameResources.Remove(resource);
		}

		private void HandleEnemyDied(Enemy enemy)
		{
			enemy.OnEnemyDied -= HandleEnemyDied;
			_objectsLocatorService.Enemies.Remove(enemy);
		}
	}
}
