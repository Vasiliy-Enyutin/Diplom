using _Project.Scripts.PlayerLogic;
using UnityEngine;

namespace _Project.Scripts.EnemyLogic
{
    public class EnemyCollisionDetector : MonoBehaviour
    {
	    public bool IsCollidesWithPlayer { get; private set; } = false;
	    
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
	            IsCollidesWithPlayer = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
	        if (other.TryGetComponent(out Player player))
	        {
		        IsCollidesWithPlayer = false;
	        }
        }
    }
}
