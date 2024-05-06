using _Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null!;
        
        [Inject]
        private InputService _inputService = null!;

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
