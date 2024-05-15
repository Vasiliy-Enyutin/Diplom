using _Project.Scripts.Descriptors;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
	[CreateAssetMenu(fileName = "Custom Installers", menuName = "Descriptor", order = 0)]
	public class DescriptorInstaller : ScriptableObjectInstaller
	{
		[SerializeField]
		private PlayerDescriptor _playerDescriptor = null!;
		[SerializeField]
		private CameraDescriptor _cameraDescriptor = null!;
		[SerializeField]
		private LocationDescriptor _locationDescriptor = null!;
		[SerializeField] 
		private MainBuildingDescriptor _mainBuildingDescriptor;
		[SerializeField] 
		private EnemyDescriptor _enemyDescriptor;
		[SerializeField]
		private UiDescriptor _uiDescriptor;
		
		public override void InstallBindings()
		{
			Container.BindInstance(_playerDescriptor).AsSingle();
			Container.BindInstance(_cameraDescriptor).AsSingle();
			Container.BindInstance(_locationDescriptor).AsSingle();
			Container.BindInstance(_mainBuildingDescriptor).AsSingle();
			Container.BindInstance(_enemyDescriptor).AsSingle();
			Container.BindInstance(_uiDescriptor).AsSingle();
		}
	}
}
