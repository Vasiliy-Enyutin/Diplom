using UnityEngine;

namespace _Project.Scripts.Descriptors
{
	[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/WeaponData")]
	public class WeaponData : ScriptableObject
	{
		public string weaponName;
		public float range;
		public int damage;
		public float reloadSpeed;
		public GameObject weaponPrefab;
	}
}