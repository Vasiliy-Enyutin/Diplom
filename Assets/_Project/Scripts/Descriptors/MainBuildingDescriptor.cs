using UnityEngine;

namespace _Project.Scripts.Descriptors
{
	[CreateAssetMenu(fileName = "MainBuildingDescriptor", menuName = "Descriptors/MainBuildingDescriptor", order = 0)]
	public class MainBuildingDescriptor : ScriptableObject
	{
		public MainBuilding Prefab = null!;
		public int Health;
	}
}