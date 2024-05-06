using UnityEngine;

namespace _Project.Scripts.Descriptors
{
	[CreateAssetMenu(fileName = "LocationDescriptor", menuName = "Descriptors/Location", order = 0)]
	public class LocationDescriptor : ScriptableObject
	{
		public Vector3 InitialPlayerPositionPoint;
		public Vector3 InitialEnemyPositionPoint;
	}
}
