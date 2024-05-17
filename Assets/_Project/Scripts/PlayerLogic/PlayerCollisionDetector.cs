using _Project.Scripts.GameResources;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
	    private void OnTriggerEnter(Collider other)
	    {
		    if (other.TryGetComponent(out ICollectible collectible))
		    {
			    (ResourceType, int) a = collectible.Collect();
			    Debug.Log(a.Item1 + " " + a.Item2);
		    }
	    }
    }
}
