using _Project.Scripts.Factories;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
	public class ServiceInstaller : MonoInstaller
	{
		[SerializeField]
		private PlayerInputService _playerInputServicePrefab = null!;
		[SerializeField]
		private UiManager _uiManager = null!;
		
		public override void InstallBindings()
		{
			Container.Bind<AssetProviderService>().AsSingle();
			Container.Bind<PlayerInputService>().FromComponentInNewPrefab(_playerInputServicePrefab).AsSingle();
			Container.Bind<UiManager>().FromComponentInNewPrefab(_uiManager).AsSingle();
			Container.Bind<GameFactoryService>().AsSingle();
		}
	}
}
