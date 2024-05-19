using _Project.Scripts.PlayerLogic;
using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;
using Menu = _Project.Scripts.UI.Panels.Menu;

namespace _Project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private UiManager _uiManager;
        private GameFactoryService _gameFactoryService;
        private LightingManager _lightingManager;
        private ObjectsLocatorService _objectsLocatorService;

        [Inject]
        private void Construct(UiManager uiManager, GameFactoryService gameFactoryService, LightingManager lightingManager, ObjectsLocatorService objectsLocatorService)
        {
	        _uiManager = uiManager;
	        _gameFactoryService = gameFactoryService;
	        _lightingManager = lightingManager;
	        _objectsLocatorService = objectsLocatorService;
        }

        private void Awake()
        {
	        Application.targetFrameRate = 60;

	        _gameFactoryService.CreateMainBuilding();
            _gameFactoryService.CreatePlayer();
            _gameFactoryService.CreateCamera();
            _objectsLocatorService.MainBuilding.OnDestruction += ShowGameOverPanel;
            _lightingManager.OnNightFalls += HandleOnNightFalls;
            _lightingManager.OnMorningComes += HandleOnMorningComes;
            _lightingManager.enabled = false;
            NavMeshSurface ground = FindObjectOfType<NavMeshSurface>();
            ground.BuildNavMesh();
        }

        private void Start()
        {
	        DisableCharactersMovement();

	        _uiManager.OnUserReadyToPlay += StartGame;
	        _uiManager.OnRestartKeyPressed += RestartLevel;
	        if (_objectsLocatorService.Player != null)
	        {
		        _objectsLocatorService.Player.OnDestroy += ShowGameOverPanel;
	        }

	        _uiManager.ShowMenu(Menu.Main);
        }

        private void OnDestroy()
        {
	        _objectsLocatorService.MainBuilding.OnDestruction -= ShowGameOverPanel;
	        _lightingManager.OnNightFalls -= HandleOnNightFalls;
	        _lightingManager.OnMorningComes -= HandleOnMorningComes;
	        _uiManager.OnUserReadyToPlay -= StartGame;
	        _uiManager.OnRestartKeyPressed -= RestartLevel;

	        if (_objectsLocatorService.Player != null)
	        {
		        _objectsLocatorService.Player.OnDestroy -= ShowGameOverPanel;
	        }
        }

        private void HandleOnMorningComes()
        {
	        _gameFactoryService.DestroyAllEnemies();
	        _gameFactoryService.CreateResources();
        }

        private void HandleOnNightFalls()
        {
	        _gameFactoryService.DestroyAllResources();

	        _gameFactoryService.CreateEnemies();
	        NavMeshSurface ground = FindObjectOfType<NavMeshSurface>();
	        ground.UpdateNavMesh(ground.navMeshData);
        }

        private void StartGame()
        {
            _uiManager.ShowMenu(Menu.InventoryView);
            _lightingManager.enabled = true;
            EnableCharactersMovement();
        }

        private void EnableCharactersMovement()
        {
	        _objectsLocatorService.Player.GetComponent<PlayerMovement>().enabled = true;
	        _objectsLocatorService.Enemies?.ForEach(enemy => enemy.GetComponent<NavMeshAgent>().enabled = true);
        }

        private void DisableCharactersMovement()
        {
	        _objectsLocatorService.Player.GetComponent<PlayerMovement>().enabled = false;
	        _objectsLocatorService.Enemies?.ForEach(enemy => enemy.GetComponent<NavMeshAgent>().enabled = false);
        }

        private void ShowGameOverPanel()
        {
            DisableCharactersMovement();
            _uiManager.ShowMenu(Menu.GameOver);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
