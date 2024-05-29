using System.Collections;
using _Project.Scripts.Descriptors;
using _Project.Scripts.PlayerLogic;
using _Project.Scripts.Services;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace TestsLogic.TestsPlayMode
{
	public class PlayerMovementTestsPlayMode
    {
        private GameObject _playerObject;
        private PlayerMovement _playerMovement;
        private InputService _inputService;
        
        [SetUp]
        public void Setup()
        {
            _playerObject = new GameObject();
            _playerObject.AddComponent<Player>();
            _playerObject.AddComponent<Rigidbody>();
            _playerMovement = _playerObject.AddComponent<PlayerMovement>();
            _inputService = new GameObject().AddComponent<InputService>();

            _playerMovement.GetComponent<Player>().Init(new PlayerDescriptor { MoveSpeed = 5f }, _inputService);
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(_playerObject);
            Object.Destroy(_inputService.gameObject);
        }

        [UnityTest]
        public IEnumerator PlayerMovesCorrectlyRight()
        {
            Vector3 initialPosition = _playerObject.transform.position;
            _inputService.MoveDirection = new Vector3(1, 0, 0);

            yield return new WaitForFixedUpdate();

            Vector3 newPosition = _playerObject.transform.position;
            Assert.AreNotEqual(initialPosition, newPosition);
            Assert.AreEqual(initialPosition.x + 5f * Time.fixedDeltaTime, newPosition.x, 0.1f);
        }

        [UnityTest]
        public IEnumerator PlayerMovesCorrectlyLeft()
        {
	        Vector3 initialPosition = _playerObject.transform.position;
	        _inputService.MoveDirection = new Vector3(-1, 0, 0);

	        yield return new WaitForFixedUpdate();

	        Vector3 newPosition = _playerObject.transform.position;
	        Assert.AreNotEqual(initialPosition, newPosition);
	        Assert.AreEqual(initialPosition.x - 5f * Time.fixedDeltaTime, newPosition.x, 0.1f);
        }
        
        [UnityTest]
        public IEnumerator PlayerMovesCorrectlyForward()
        {
	        Vector3 initialPosition = _playerObject.transform.position;
	        _inputService.MoveDirection = new Vector3(0, 0, 1);

	        yield return new WaitForFixedUpdate();

	        Vector3 newPosition = _playerObject.transform.position;
	        Assert.AreNotEqual(initialPosition, newPosition);
	        Assert.AreEqual(initialPosition.z + 5f * Time.fixedDeltaTime, newPosition.x, 0.1f);
        }
        
        [UnityTest]
        public IEnumerator PlayerMovesCorrectlyBackwards()
        {
	        Vector3 initialPosition = _playerObject.transform.position;
	        _inputService.MoveDirection = new Vector3(0, 0, -1);

	        yield return new WaitForFixedUpdate();

	        Vector3 newPosition = _playerObject.transform.position;
	        Assert.AreNotEqual(initialPosition, newPosition);
	        Assert.AreEqual(initialPosition.z - 5f * Time.fixedDeltaTime, newPosition.x, 0.1f);
        }
    }
}