using System.Collections;
using _Project.Scripts.Descriptors;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.Services;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace TestsLogic.TestsPlayMode
{
	public class PlayerTestsPlayMode
	{
	    private Player _player;
	    private PlayerDescriptor _playerDescriptor;
	    private InputService _inputService;
	    private int _initialHealth;

	    [SetUp]
	    public void Setup()
	    {
	        _player = new GameObject().AddComponent<Player>();
	        _playerDescriptor = ScriptableObject.CreateInstance<PlayerDescriptor>();
	        _playerDescriptor.Health = 100;
	        _playerDescriptor.RestoreHealthAmount = 10;
	        _playerDescriptor.RestoreHealthInterval = 1.0f;
	        _inputService = new GameObject().AddComponent<InputService>();
	        
	        _player.OnPlayerHealthChanged += newHealth => _initialHealth = newHealth;
	        _player.Init(_playerDescriptor, _inputService);
	    }

	    [TearDown]
	    public void Teardown()
	    {
	        if (_player.gameObject != null)
	        {
	            Object.Destroy(_player.gameObject);
	        }
	        if (_playerDescriptor != null)
	        {
	            Object.Destroy(_playerDescriptor);
	        }

	        if (_inputService.gameObject != null)
	        {
		        Object.Destroy(_inputService.gameObject);
	        }
	    }

	    [UnityTest]
	    public IEnumerator PlayerInitialization()
	    {
		    yield return null; // Даем кадру завершиться
		    Assert.AreEqual(100, _initialHealth);
	    }

	    [UnityTest]
	    public IEnumerator PlayerHealthRestoration()
	    {
		    int health = 0;
		    _player.OnPlayerHealthChanged += newHealth => health = newHealth;

		    _player.TakeDamage(50);
		    yield return new WaitForSeconds(1.1f);

		    Assert.AreEqual(60, health);

		    yield return new WaitForSeconds(4.1f);

		    Assert.AreEqual(100, health);
	    }
	    
	    [UnityTest]
	    public IEnumerator PlayerHealthDoesNotExceedBaseHealth()
	    {
		    int health = 0;
		    _player.OnPlayerHealthChanged += newHealth => health = newHealth;

		    _player.TakeDamage(20);
		    yield return new WaitForSeconds(5f);

		    Assert.AreEqual(100, health);
	    }
	    
	    [UnityTest]
	    public IEnumerator PlayerTakesDamageAfterRestoration()
	    {
		    int health = 0;
		    _player.OnPlayerHealthChanged += newHealth => health = newHealth;

		    _player.TakeDamage(10);
		    yield return new WaitForSeconds(2f);

		    _player.TakeDamage(10);
		    yield return null;

		    Assert.AreEqual(90, health);
	    }
	}
}
