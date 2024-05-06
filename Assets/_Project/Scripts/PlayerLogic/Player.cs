using System;
using _Project.Scripts.Descriptors;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class Player : MonoBehaviour
    {
	    public event Action OnDestroy;

	    public PlayerDescriptor PlayerDescriptor { get; private set; }

	    public void Init(PlayerDescriptor playerDescriptor)
	    {
		    PlayerDescriptor = playerDescriptor;
	    }
        
        public void Die()
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}
