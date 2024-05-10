using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Descriptors
{
	[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Weapons/WeaponDatabase")]
	public class WeaponDatabase : ScriptableObject
	{
		public List<WeaponData> Weapons = new();
	}
}