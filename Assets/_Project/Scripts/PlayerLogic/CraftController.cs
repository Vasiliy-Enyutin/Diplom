using System;
using _Project.Scripts.GameResources;
using _Project.Scripts.PlayerLogic.InventoryLogic;

namespace _Project.Scripts.PlayerLogic
{
	public class CraftController
	{
		private readonly MainBuilding _mainBuilding;
		private readonly InventoryController _inventoryController;

		public CraftController(MainBuilding mainBuilding, InventoryController inventoryController)
		{
			_mainBuilding = mainBuilding;
			_inventoryController = inventoryController;
		}
		
		public void CreateAmmo()
		{
			int bulletsCount = 0;

			int coalCount = _inventoryController.GetAllResourceByType(ResourceType.Coal);
			int leadCount = _inventoryController.GetAllResourceByType(ResourceType.Lead);
			int sulfurCount = _inventoryController.GetAllResourceByType(ResourceType.Sulfur);

			int minResourceCount = Math.Min(coalCount, Math.Min(leadCount, sulfurCount));

			// Увеличиваем bulletsCount на количество возможных патронов
			bulletsCount += minResourceCount;
		}

		public void RepairBase()
		{
			int woodAmountForCompleteRepair = _mainBuilding.BaseHealth - _mainBuilding.CurrentHealth;
			int woodCount = _inventoryController.GetSpecifiedResourceAmountOrLess(ResourceType.Wood, woodAmountForCompleteRepair);
			_mainBuilding.Repair(woodCount);
		}
	}
}