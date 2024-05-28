using System.Collections.Generic;
using _Project.Scripts.GameResources;

namespace _Project.Scripts.PlayerLogic.InventoryLogic
{
	public class InventoryModel
	{
		private readonly Dictionary<ResourceType, int> _resourceTypeByAmount = new();

		public void AddResource(ResourceType resourceType, int amount)
		{
			if (_resourceTypeByAmount.ContainsKey(resourceType))
			{
				_resourceTypeByAmount[resourceType] += amount;
			}
			else
			{
				_resourceTypeByAmount[resourceType] = amount;
			}
		}

		public int LookResourceAmount(ResourceType resourceType)
		{
			if (_resourceTypeByAmount.TryGetValue(resourceType, out int currentAmount))
			{
				return currentAmount;
			}

			return 0;
		}

		public int GetResource(ResourceType resourceType, int amount)
		{
			if (!_resourceTypeByAmount.TryGetValue(resourceType, out int currentAmount))
			{
				return 0;
			}

			if (currentAmount < amount)
			{
				_resourceTypeByAmount[resourceType] -= currentAmount;
				return currentAmount;
			}
			
			_resourceTypeByAmount[resourceType] -= amount;
			return amount;
		}
	}
}
