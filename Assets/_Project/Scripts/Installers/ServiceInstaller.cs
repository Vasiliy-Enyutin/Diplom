using _Project.Scripts.Services;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
	public class ServiceInstaller : MonoInstaller
	{
		[SerializeField]
		private InputService _inputServicePrefab = null!;
		[SerializeField]
		private UiManager _uiManager = null!;
		[SerializeField]
		private LightingManager _lightingManagerPrefab = null!;
		[SerializeField]
		private PursuitMusicController _pursuitMusicController = null!;
		
		public override void InstallBindings()
		{
			Container.Bind<LightingManager>().FromComponentInNewPrefab(_lightingManagerPrefab).AsSingle();
			Container.Bind<AssetProviderService>().AsSingle();
			Container.Bind<InputService>().FromComponentInNewPrefab(_inputServicePrefab).AsSingle();
			Container.Bind<UiManager>().FromComponentInNewPrefab(_uiManager).AsSingle();
			Container.Bind<GameFactoryService>().AsSingle();
			Container.Bind<PursuitMusicController>().FromComponentInNewPrefab(_pursuitMusicController).AsSingle();
		}
	}
}
