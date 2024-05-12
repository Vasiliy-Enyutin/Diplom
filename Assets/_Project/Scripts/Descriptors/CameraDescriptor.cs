using UnityEngine;

namespace _Project.Scripts.Descriptors
{
	[CreateAssetMenu(fileName = "CameraDescriptor", menuName = "Descriptors/CameraDescriptor", order = 0)]
	public class CameraDescriptor : ScriptableObject
	{
		public GameObject CameraPrefab;
		public Vector3 CameraOffset;
		public Quaternion CameraRotation;
	}
}