using System;
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
        [Inject]
        private UiManager _uiManager;
        [Inject]
        private GameFactoryService _gameFactoryService;

        private void Awake()
        {
            Cursor.visible = false;
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            DisableCharactersMovement();

            _uiManager.OnUserReadyToPlay += StartGame;
            _uiManager.OnNextLevelKeyPressed += GoToNextLevel;
            _uiManager.OnRestartKeyPressed += RestartLevel;

            Player player = _gameFactoryService.Player;
            player.OnReachedFinish += ShowNextLevelPanel;
            player.OnDestroy += ShowGameOverPanel;
            
            _uiManager.ShowMenu(Menu.Main);
        }

        private void OnDisable()
        {
            _uiManager.OnUserReadyToPlay -= StartGame;
            _uiManager.OnRestartKeyPressed -= RestartLevel;

            if (_gameFactoryService.Player != null)
            {
                _gameFactoryService.Player.OnDestroy -= ShowGameOverPanel;
            }
        }

        private void StartGame()
        {
            _uiManager.HideAll();
            EnableCharactersMovement();
        }

        private void EnableCharactersMovement()
        {
            _gameFactoryService.Player.GetComponent<PlayerMovement>().enabled = true;
            _gameFactoryService.Enemies.ForEach(enemy => enemy.GetComponent<NavMeshAgent>().enabled = true);
        }

        private void DisableCharactersMovement()
        {
            _gameFactoryService.Player.GetComponent<PlayerMovement>().enabled = false;
            _gameFactoryService.Enemies.ForEach(enemy => enemy.GetComponent<NavMeshAgent>().enabled = false);
        }

        private void ShowGameOverPanel()
        {
            DisableCharactersMovement();
            _uiManager.ShowMenu(Menu.GameOver);
        }

        private void ShowNextLevelPanel()
        {
            _uiManager.ShowMenu(Menu.Win);
        }

        private void GoToNextLevel()
        {
            //TODO Пока нет других уровней делаю рестарт текущего
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
