using System;
using _Project.Scripts.Descriptors;
using _Project.Scripts.UI.Panels;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services
{
    public class UiManager : MonoBehaviour
    {
        private AssetProviderService _assetProviderService;
        private UiDescriptor _uiDescriptor;
        private ObjectsLocatorService _objectsLocatorService;

        public event Action OnUserReadyToPlay;
        public event Action OnRestartKeyPressed;

        private MainMenuPanel _mainMenuPanel;
        private GameOverPanel _gameOverPanel;
        private InventoryViewPanel _inventoryViewPanel;

        [Inject]
        private void Construct(AssetProviderService assetProviderService, UiDescriptor uiDescriptor, ObjectsLocatorService objectsLocatorService)
        {
	        _assetProviderService = assetProviderService;
	        _uiDescriptor = uiDescriptor;
	        _objectsLocatorService = objectsLocatorService;
        }

        private void Start()
        {
            _mainMenuPanel = _assetProviderService.CreateAsset<MainMenuPanel>(_uiDescriptor.MainMenuPanelPrefab, transform);
            _gameOverPanel = _assetProviderService.CreateAsset<GameOverPanel>(_uiDescriptor.GameOverPanelPrefab, transform);
            
            _inventoryViewPanel = _assetProviderService.CreateAsset<InventoryViewPanel>(_uiDescriptor.InventoryViewPanelPrefab, transform);
            _inventoryViewPanel.Init(_objectsLocatorService.MainBuilding, _objectsLocatorService.Player);

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
            else if (menu == Menu.InventoryView)
            {
	            _inventoryViewPanel.Show();
            }
        }

        private void HideAll()
        {
            _mainMenuPanel.Hide();;
            _gameOverPanel.Hide();;
            _inventoryViewPanel.Hide();;
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
