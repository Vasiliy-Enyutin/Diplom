using _Project.Scripts.Descriptors;
using UnityEngine;

namespace _Project.Scripts
{
	public class MainBuilding : MonoBehaviour, IDamageable
	{
		private float _health;
		
		public void Init(MainBuildingDescriptor mainBuildingDescriptor)
		{
			_health = mainBuildingDescriptor.Health;
		}

		public void TakeDamage(float damage)
		{
			Debug.Log("MainBuilding get hit");
			_health -= damage;
		
			if (_health <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			Destroy(gameObject);
		}
	}
}
