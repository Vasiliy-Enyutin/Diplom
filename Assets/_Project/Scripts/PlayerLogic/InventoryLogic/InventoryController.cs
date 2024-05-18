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

		public bool TryGetResource(ResourceType resourceType, int amount)
		{
			bool success = _inventoryModel.TryGetResource(resourceType, amount);
			if (success)
			{
				OnResourceAmountChanged?.Invoke(resourceType, amount);
			}
			return success;
		}
	}
}
