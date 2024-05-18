using System;
using _Project.Scripts.Descriptors;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class Player : MonoBehaviour, IDamageable
    {
	    public event Action<int> OnPlayerHealthChanged;
	    
	    private int _currentHealth;
	    private int _baseHealth;

	    public event Action OnDestroy;

	    public PlayerDescriptor PlayerDescriptor { get; private set; }
	    public InputService InputService { get; private set; }

	    public void Init(PlayerDescriptor playerDescriptor, InputService inputService)
	    {
		    _currentHealth = playerDescriptor.Health;
		    _baseHealth = _currentHealth;
		    OnPlayerHealthChanged?.Invoke(_currentHealth);
		    
		    PlayerDescriptor = playerDescriptor;
		    InputService = inputService;
	    }

	    public void TakeDamage(int damage)
	    {
		    _currentHealth -= damage;
		    OnPlayerHealthChanged?.Invoke(_currentHealth);
	    
		    if (_currentHealth <= 0)
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
