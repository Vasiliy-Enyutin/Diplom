using System;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Services;
using _Project.Scripts.UI.Panels;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        [Inject]
        private AssetProviderService _assetProviderService = null!;
        [Inject]
        private UiDescriptor _uiDescriptor = null!;

        public event Action OnUserReadyToPlay;
        public event Action OnRestartKeyPressed;

        private MainMenuPanel _mainMenuPanel;
        private GameOverPanel _gameOverPanel;

        private void Awake()
        {
            _mainMenuPanel = _assetProviderService.CreateAsset<MainMenuPanel>(_uiDescriptor.MainMenuPanelPrefab, transform);
            _gameOverPanel = _assetProviderService.CreateAsset<GameOverPanel>(_uiDescriptor.GameOverPanelPrefab, transform);

            _mainMenuPanel.OnPlayerAnyKeyDown += InvokeUserReadyToPlay;
            _gameOverPanel.OnRestartKeyDown += InvokeRestart;

            HideAll();
        }

        private void OnDestroy()
        {
            _mainMenuPanel.OnPlayerAnyKeyDown -= InvokeUserReadyToPlay;
            _gameOverPanel.OnRestartKeyDown -= InvokeRestart;
        }

        public void ShowMenu(Menu menu)
        {
            HideAll();
            
            if (menu == Menu.Main)
            {
                _mainMenuPanel.Show();
            }
            else if (menu == Menu.GameOver)
            {
                _gameOverPanel.Show();
            }
        }

        public void HideAll()
        {
            _mainMenuPanel.Hide();;
            _gameOverPanel.Hide();;
        }

        private void InvokeUserReadyToPlay()
        {
            OnUserReadyToPlay?.Invoke();
        }

        private void InvokeRestart()
        {
            OnRestartKeyPressed?.Invoke();
        }
    }
}
