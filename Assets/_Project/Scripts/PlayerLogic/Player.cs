using System;
using System.Collections;
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
	    private bool _isRestoringHealth;

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
		    
		    if (!_isRestoringHealth)
		    {
			    StartCoroutine(RestoreHealthOverTime(PlayerDescriptor.RestoreHealthAmount,
				    PlayerDescriptor.RestoreHealthInterval));
		    }
	    }

	    private void Die()
        {
	        OnDestroy?.Invoke();
	        if (Application.isPlaying)
	        {
		        Destroy(gameObject);
	        }
        }
	    
	    private IEnumerator RestoreHealthOverTime(int amount, float interval)
	    {
		    _isRestoringHealth = true;
		    while (_currentHealth < _baseHealth)
		    {
			    _currentHealth += amount;
			    if (_currentHealth > _baseHealth)
			    {
				    _currentHealth = _baseHealth;
			    }
			    OnPlayerHealthChanged?.Invoke(_currentHealth);
			    yield return new WaitForSeconds(interval);
		    }

		    _isRestoringHealth = false;
	    }
    }
}
