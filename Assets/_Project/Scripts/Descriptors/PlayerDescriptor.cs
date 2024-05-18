using _Project.Scripts.PlayerLogic;
using UnityEngine;

namespace _Project.Scripts.Descriptors
{
	[CreateAssetMenu(fileName = "PlayerDescriptor", menuName = "Descriptors/Player", order = 0)]
	public class PlayerDescriptor : ScriptableObject
	{
		public Player Prefab = null!;
		public int Health;
		public float MoveSpeed;
		public WeaponDatabase WeaponDatabase;
		public float RestoreHealthInterval;
		public int RestoreHealthAmount;
	}
}
