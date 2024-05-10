using System;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class Player : MonoBehaviour
    {
	    public event Action OnDestroy;

	    public PlayerDescriptor PlayerDescriptor { get; private set; }
	    public InputService InputService { get; private set; }

	    public void Init(PlayerDescriptor playerDescriptor, InputService inputService)
	    {
		    PlayerDescriptor = playerDescriptor;
		    InputService = inputService;
	    }
        
        public void Die()
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}
