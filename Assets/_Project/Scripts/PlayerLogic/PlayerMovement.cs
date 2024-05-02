using _Project.Scripts.Descriptors;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private Transform _playerGfxTransform;
        
        [Inject]
        private PlayerInputService _playerInputService = null!;
        [Inject]
        private PlayerDescriptor _playerDescriptor = null!;

        private Rigidbody _rigidbody = null!;
        
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_playerInputService == null || _playerDescriptor == null)
            {
                return;
            }

            Move(_playerInputService.MoveDirection, _playerDescriptor.MoveSpeed);
            RotatePlayer();
        }

        private void RotatePlayer()
        {
            if (_moveDirection.magnitude > 0)
            {
                _playerGfxTransform.rotation = Quaternion.LookRotation(_moveDirection);
            }
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

        public void ConstructTest(Transform playerGfxTransform, Vector3 moveDirection, float moveSpeed, Rigidbody rigidbody)
        {
            _playerGfxTransform = playerGfxTransform;
            _rigidbody = rigidbody;
            
            Move(moveDirection, moveSpeed);
            RotatePlayer();
        }

        public Vector3 GetMoveDirectionTest()
        {
            return _moveDirection;
        }
    }
}
