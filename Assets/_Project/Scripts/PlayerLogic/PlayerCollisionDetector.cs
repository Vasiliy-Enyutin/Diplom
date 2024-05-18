using _Project.Scripts.GameResources;
using _Project.Scripts.PlayerLogic.InventoryLogic;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
	    private InventoryController _inventoryController;
	    
	    private void Awake()
	    {
		    _inventoryController = GetComponent<InventoryController>();
	    }

	    private void OnTriggerEnter(Collider other)
	    {
		    if (other.TryGetComponent(out ICollectible collectible))
		    {
			    (ResourceType, int) resource = collectible.Collect();
			    Debug.Log(resource.Item1 + " " + resource.Item2);
			    _inventoryController.AddResource(resource.Item1, resource.Item2);
		    }
	    }
    }
}
