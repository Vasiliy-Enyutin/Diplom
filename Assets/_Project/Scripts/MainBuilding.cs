using _Project.Scripts.Descriptors;
using UnityEngine;

namespace _Project.Scripts
{
	public class MainBuilding : MonoBehaviour, IDamageable
	{
		public int BaseHealth { get; private set; }

		public int CurrentHealth { get; private set; }

		public void Init(MainBuildingDescriptor mainBuildingDescriptor)
		{
			CurrentHealth = mainBuildingDescriptor.Health;
			BaseHealth = mainBuildingDescriptor.Health;
		}

		public void Repair(int hp)
		{
			if (CurrentHealth + hp > BaseHealth)
			{
				CurrentHealth = BaseHealth;
			}
			else
			{
				CurrentHealth += hp;
			}
		}

		public void TakeDamage(int damage)
		{
			Debug.Log("MainBuilding get hit");
			CurrentHealth -= damage;
		
			if (CurrentHealth <= 0)
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
