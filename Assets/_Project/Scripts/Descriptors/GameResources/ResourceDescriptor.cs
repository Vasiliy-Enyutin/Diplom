using _Project.Scripts.GameResources;
using UnityEngine;

namespace _Project.Scripts.Descriptors.GameResources
{
	[CreateAssetMenu(fileName = "Resources", menuName = "Resources/ResourceDescriptor", order = 0)]
	public class ResourceDescriptor : ScriptableObject
	{
		public GameResource ResourcePrefab;
		public ResourceType ResourceType;
		public int ResourcesNumberOnMap;
		public int ResourcesAmount;
		public Vector3[] SpawnPoints;
	}
}