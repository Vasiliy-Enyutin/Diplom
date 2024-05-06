using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
	public class ServiceInstaller : MonoInstaller
	{
		[SerializeField]
		private InputService inputServicePrefab = null!;
		[SerializeField]
		private UiManager _uiManager = null!;
		
		public override void InstallBindings()
		{
			Container.Bind<AssetProviderService>().AsSingle();
			Container.Bind<InputService>().FromComponentInNewPrefab(inputServicePrefab).AsSingle();
			Container.Bind<UiManager>().FromComponentInNewPrefab(_uiManager).AsSingle();
			Container.Bind<GameFactoryService>().AsSingle();
		}
	}
}
