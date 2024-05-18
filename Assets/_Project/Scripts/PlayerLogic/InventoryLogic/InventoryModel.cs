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

		public bool TryGetResource(ResourceType resourceType, int amount)
		{
			if (_resourceTypeByAmount.TryGetValue(resourceType, out int currentAmount))
			{
				if (currentAmount >= amount)
				{
					_resourceTypeByAmount[resourceType] -= amount;
					return true;
				}
			}
			return false;
		}
	}
}
