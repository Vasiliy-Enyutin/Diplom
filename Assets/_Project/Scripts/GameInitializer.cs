using _Project.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Project.Scripts
{
	public class GameInitializer : MonoBehaviour
	{
		[Inject]
		private GameFactoryService _gameFactoryService = null!;


		private void Awake()
		{
			_gameFactoryService.CreatePlayer();
			_gameFactoryService.CreateCamera();
			_gameFactoryService.CreateEnemies();
			FindObjectOfType<NavMeshSurface>().BuildNavMesh();
		}
	}
}
