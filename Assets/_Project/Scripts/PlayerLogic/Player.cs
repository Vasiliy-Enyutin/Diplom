using System;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class Player : MonoBehaviour, IDamageable
    {
	    private float _health;
	    
	    public event Action OnDestroy;

	    public PlayerDescriptor PlayerDescriptor { get; private set; }
	    public InputService InputService { get; private set; }

	    public void Init(PlayerDescriptor playerDescriptor, InputService inputService)
	    {
		    _health = playerDescriptor.Health;
		    
		    PlayerDescriptor = playerDescriptor;
		    InputService = inputService;
	    }

	    public void TakeDamage(float damage)
	    {
		    Debug.Log("Player get hit. Damage: " + damage);
		    _health -= damage;
	    
		    if (_health <= 0)
		    {
			    Die();
		    }
	    }

	    private void Die()
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}
