using System;
using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null!;

        private InputService _inputService;

        private void Start()
        {
	        _inputService = GetComponent<Player>().InputService;
        }

        private void Update()
        {
            if (_inputService == null)
            {
                return;
            }
            
            UpdateAnimation(_inputService.MoveDirection);
        }

        private void UpdateAnimation(Vector3 moveDirection)
        {
            _animator.Play(moveDirection == Vector3.zero ? "Idle" : "Run");
        }
    }
}
