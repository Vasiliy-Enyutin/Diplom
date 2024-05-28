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
			int bulletsCount = _inventoryController.GetBulletsNumberThatCanBeCreated();
			_inventoryController.AddResource(ResourceType.Bullet, bulletsCount);
		}

		public void RepairBase()
		{
			int woodAmountForCompleteRepair = _mainBuilding.BaseHealth - _mainBuilding.CurrentHealth;
			int woodCount = _inventoryController.GetSpecifiedResourceAmountOrLess(ResourceType.Wood, woodAmountForCompleteRepair);
			_mainBuilding.Repair(woodCount);
		}
	}
}