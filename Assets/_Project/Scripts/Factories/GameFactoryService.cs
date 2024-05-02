using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Descriptors;
using _Project.Scripts.EnemyLogic;
using _Project.Scripts.PlayerLogic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Factories
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

		// Из-за своеобразного спавна лабиринта пол ячеек может выходить за пределы лабиринта
		private const int CELLS_COORDS_OUTSIDE_LABYRINTH = 45;
		private const int START_POSITION = 0;
		private const int START_POSITION2 = 5;

		public Player Player { get; private set; }

		public List<Enemy> Enemies { get; } = new();

		public void CreatePlayer()
		{
			Player = _assetProviderService.CreateAsset<Player>(_playerDescriptor.Prefab, _locationDescriptor.InitialPlayerPositionPoint);
		}

		public void CreateEnemies(List<Vector3> cellsPositions)
		{
			List<Vector3> spawnPositions = cellsPositions
				.Where(cellPosition =>
					!(Math.Abs(cellPosition.x - START_POSITION) < Mathf.Epsilon &&
					  Math.Abs(cellPosition.z - START_POSITION) < Mathf.Epsilon) &&
					!(Math.Abs(cellPosition.x - START_POSITION) < Mathf.Epsilon &&
					  Math.Abs(cellPosition.z - START_POSITION2) < Mathf.Epsilon) &&
					!(Math.Abs(cellPosition.x - START_POSITION2) < Mathf.Epsilon &&
					  Math.Abs(cellPosition.z - START_POSITION) < Mathf.Epsilon) &&
					!(Math.Abs(cellPosition.x - CELLS_COORDS_OUTSIDE_LABYRINTH) < Mathf.Epsilon ||
					  Math.Abs(cellPosition.z - CELLS_COORDS_OUTSIDE_LABYRINTH) < Mathf.Epsilon)
				)
				.OrderBy(_ => Guid.NewGuid()).Take(_enemyDescriptor.EnemiesNumber).ToList();

			
			foreach (Vector3 spawnPosition in spawnPositions)
			{
				Enemy enemy = _assetProviderService.CreateAsset<Enemy>(_enemyDescriptor.Enemy, spawnPosition);
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
