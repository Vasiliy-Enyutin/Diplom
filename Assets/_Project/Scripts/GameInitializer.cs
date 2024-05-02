using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Factories;
using _Project.Scripts.LabyrinthLogic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.Scripts
{
	public class GameInitializer : MonoBehaviour
	{
		[Inject]
		private GameFactoryService _gameFactoryService = null!;
		[Inject]
		private LabyrinthDescriptor _labyrinthDescriptor = null!;


		private void Awake()
		{
			_gameFactoryService.CreatePlayer();
			InitLabyrinth();
			SpawnEnemies();
			BuildNavMesh();
		}

		private void InitLabyrinth()
		{
			FindObjectOfType<LabyrinthSpawner>().Init(_labyrinthDescriptor.CellPrefab, _labyrinthDescriptor.CellSize,
				_labyrinthDescriptor.LabyrinthWidth, _labyrinthDescriptor.LabyrinthHeight);
		}

		private void SpawnEnemies()
		{
			List<Vector3> cellsPositions = FindObjectsOfType<Cell>().Select(cell => cell.transform.position).ToList();
			_gameFactoryService.CreateEnemies(cellsPositions);
		}

		private void BuildNavMesh()
		{
			FindObjectOfType<NavMeshSurface>().BuildNavMesh();
		}
	}
}
