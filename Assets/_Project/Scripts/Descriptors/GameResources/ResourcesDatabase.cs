using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Descriptors.GameResources
{
	[CreateAssetMenu(fileName = "ResourcesDatabase", menuName = "Resources/ResourcesDatabase")]
	public class ResourcesDatabase : ScriptableObject
	{
		public List<ResourceDescriptor> Resources = new();
		public Vector3[] SpawnPoints;
	}
}