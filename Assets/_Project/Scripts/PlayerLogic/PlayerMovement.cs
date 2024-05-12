using _Project.Scripts.Descriptors;
using _Project.Scripts.Services;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private Transform _playerGfxTransform;
        
        private PlayerDescriptor _playerDescriptor = null!;
        private InputService _inputService = null!;
        private Rigidbody _rigidbody = null!;
        private Vector3 _moveDirection;
        
        private Camera _mainCamera;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _mainCamera = FindObjectOfType<CinemachineBrain>().GetComponent<Camera>();
        }

        private void Start()
        {
            _playerDescriptor = GetComponent<Player>().PlayerDescriptor;
            _inputService = GetComponent<Player>().InputService;
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
            if (_mainCamera == null)
            {
                Debug.Log("Main Camera is missing");
                return;
            }
        
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new(Vector3.up, transform.position);
        
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPoint = ray.GetPoint(distance);
                Vector3 direction = targetPoint - transform.position;
                direction.y = 0f;
        
                if (direction.magnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    _playerGfxTransform.rotation = Quaternion.Slerp(_playerGfxTransform.rotation, targetRotation, Time.deltaTime * 10f);
                }
            }
        }
    }
}