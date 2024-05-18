﻿using _Project.Scripts.GameResources;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.PlayerLogic.InventoryLogic;
using UnityEngine;
using TMPro;

namespace _Project.Scripts.UI.Panels
{
	public class InventoryViewPanel : Panel
	{
		[SerializeField]
		private TextMeshProUGUI  _coalAmountText;
		[SerializeField]
		private TextMeshProUGUI  _leadAmountText;
		[SerializeField]
		private TextMeshProUGUI  _sulfurAmountText;
		[SerializeField]
		private TextMeshProUGUI  _woodAmountText;
		
		private InventoryController _inventoryController;
		private CraftController _craftController;
		
		public void Init(MainBuilding mainBuilding, InventoryController inventoryController)
		{
			_inventoryController = inventoryController;
			_craftController = new CraftController(mainBuilding, inventoryController);

			_inventoryController.OnResourceAmountChanged += ChangeResourceAmountUI;
		}

		private void OnDestroy()
		{
			_inventoryController.OnResourceAmountChanged -= ChangeResourceAmountUI;
		}

		public void OnCreateAmmoButtonClicked()
		{
			_craftController.CreateAmmo();
		}

		public void OnRepairBaseButtonClicked()
		{
			_craftController.RepairBase();
		}

		private void ChangeResourceAmountUI(ResourceType resourceType, int amount)
		{
			switch (resourceType)
			{
				case ResourceType.Coal:
					int coalAmount = int.Parse(_coalAmountText.text);
					_coalAmountText.text = (coalAmount + amount).ToString();
					break;
				case ResourceType.Lead:
					int leadAmount = int.Parse(_leadAmountText.text);
					_leadAmountText.text = (leadAmount + amount).ToString();
					break;
				case ResourceType.Wood:
					int woodAmount = int.Parse(_woodAmountText.text);
					_woodAmountText.text = (woodAmount + amount).ToString();
					break;
				case ResourceType.Sulfur:
					int sulfurAmount = int.Parse(_sulfurAmountText.text);
					_sulfurAmountText.text = (sulfurAmount + amount).ToString();
					break;
			}
		}
	}
}