using System;
using UnityEngine;

namespace _Project.Scripts.GameResources
{
	public class GameResource : MonoBehaviour, ICollectible
	{
		public event Action<GameResource> OnGameResourceCollected;
		
		private ResourceType _resourceType;
		private int _amount;
		
		public void Init(ResourceType resourceType, int amount)
		{
			_resourceType = resourceType;
			_amount = amount;
		}

		public (ResourceType, int) Collect()
		{
			Die();
			return (_resourceType, _amount);
		}

		private void Die()
		{
			OnGameResourceCollected?.Invoke(this);
			Destroy(gameObject);
		}
	}
}
