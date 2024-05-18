using System;
using _Project.Scripts.GameResources;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic.InventoryLogic
{
	public class InventoryController : MonoBehaviour
	{
		public event Action<ResourceType, int> OnResourceAmountChanged;

		private readonly InventoryModel _inventoryModel = new();

		public void AddResource(ResourceType resourceType, int amount)
		{
			_inventoryModel.AddResource(resourceType, amount);
			OnResourceAmountChanged?.Invoke(resourceType, amount);
		}

		public int GetSpecifiedResourceAmountOrLess(ResourceType resourceType, int amount)
		{
			return _inventoryModel.GetSpecifiedResourceAmountOrLess(resourceType, amount);
		}

		public int GetAllResourceByType(ResourceType resourceType)
		{
			int amount = _inventoryModel.GetAllResourceByType(resourceType);
			if (amount > 0)
			{
				OnResourceAmountChanged?.Invoke(resourceType, -amount);
				return amount;
			}
			
			return 0;
		}
	}
}
