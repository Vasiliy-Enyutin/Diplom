using _Project.Scripts.Descriptors;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private Transform _playerGfxTransform;
        
        [Inject]
        private InputService _inputService = null!;

        private PlayerDescriptor _playerDescriptor;
        private Rigidbody _rigidbody = null!;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
	        _playerDescriptor = GetComponent<Player>().PlayerDescriptor;
        }

        private void FixedUpdate()
        {
	        if (_inputService == null || _playerDescriptor == null)
            {
                return;
            }
            
            Move(_inputService.MoveDirection, _playerDescriptor.MoveSpeed);
            RotatePlayer();
        }

        private void Move(Vector3 moveDirection, float moveSpeed)
        {
            _moveDirection = transform.right * moveDirection.x +
                             transform.forward * moveDirection.z;

            if (_moveDirection.magnitude > 1)
            {
                _moveDirection.Normalize();
            }

            _rigidbody.MovePosition(transform.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        private void RotatePlayer()
        {
	        if (_moveDirection.magnitude > 0)
	        {
		        _playerGfxTransform.rotation = Quaternion.LookRotation(_moveDirection);
	        }
        }
    }
}
