using System;
using _Project.Scripts.Descriptors;
using UnityEngine;

namespace _Project.Scripts
{
	public class MainBuilding : MonoBehaviour, IDamageable
	{
		public event Action<int> OnMainBuildingHealthChanged;

		public int BaseHealth { get; private set; }

		public int CurrentHealth { get; private set; }

		public void Init(MainBuildingDescriptor mainBuildingDescriptor)
		{
			CurrentHealth = mainBuildingDescriptor.Health;
			BaseHealth = mainBuildingDescriptor.Health;
			OnMainBuildingHealthChanged?.Invoke(CurrentHealth);
		}

		public void Repair(int hp)
		{
			if (CurrentHealth + hp > BaseHealth)
			{
				CurrentHealth = BaseHealth;
			}
			else
			{
				OnMainBuildingHealthChanged?.Invoke(CurrentHealth);
				CurrentHealth += hp;
			}
			OnMainBuildingHealthChanged?.Invoke(CurrentHealth);
		}

		public void TakeDamage(int damage)
		{
			CurrentHealth -= damage;
			OnMainBuildingHealthChanged?.Invoke(CurrentHealth);

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
