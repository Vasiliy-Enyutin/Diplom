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
			int receivedResourceAmount = _inventoryModel.GetResource(resourceType, amount);
			OnResourceAmountChanged?.Invoke(resourceType, -receivedResourceAmount);
			return receivedResourceAmount;
		}

		public int GetBulletsNumberThatCanBeCreated()
		{
			int coalCount = _inventoryModel.LookResourceAmount(ResourceType.Coal);
			int leadCount = _inventoryModel.LookResourceAmount(ResourceType.Lead);
			int sulfurCount = _inventoryModel.LookResourceAmount(ResourceType.Sulfur);

			int minResourceCount = Mathf.Min(coalCount, Mathf.Min(leadCount, sulfurCount));

			OnResourceAmountChanged?.Invoke(ResourceType.Coal, -_inventoryModel.GetResource(ResourceType.Coal, minResourceCount));
			OnResourceAmountChanged?.Invoke(ResourceType.Lead, -_inventoryModel.GetResource(ResourceType.Lead, minResourceCount));
			OnResourceAmountChanged?.Invoke(ResourceType.Sulfur, -_inventoryModel.GetResource(ResourceType.Sulfur, minResourceCount));

			return minResourceCount;
		}

		// public int GetSpecifiedResourceAmountOrLess(ResourceType resourceType, int amount)
		// {
		// 	return _inventoryModel.GetSpecifiedResourceAmountOrLess(resourceType, amount);
		// }
		//
		// public int GetAllResourceByType(ResourceType resourceType)
		// {
		// 	int amount = _inventoryModel.GetAllResourceByType(resourceType);
		// 	if (amount > 0)
		// 	{
		// 		OnResourceAmountChanged?.Invoke(resourceType, -amount);
		// 		return amount;
		// 	}
		// 	
		// 	return 0;
		// }
	}
}
