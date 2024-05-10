using _Project.Scripts.PlayerLogic.AttackLogic;
using UnityEngine;

namespace _Project.Scripts.Descriptors
{
	[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/WeaponData")]
	public class WeaponData : ScriptableObject
	{
		public string WeaponName;
		public WeaponType WeaponType;
		public float Range;
		public int Damage;
		public float ReloadSpeed;
		public GameObject WeaponPrefab;
	}
}