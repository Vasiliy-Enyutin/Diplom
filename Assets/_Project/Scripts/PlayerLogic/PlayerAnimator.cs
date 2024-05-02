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
        private PlayerInputService _playerInputService = null!;

        private void Update()
        {
            if (_playerInputService == null)
            {
                return;
            }
            
            UpdateAnimation(_playerInputService.MoveDirection);
        }

        private void UpdateAnimation(Vector3 moveDirection)
        {
            _animator.Play(moveDirection == Vector3.zero ? "Idle" : "Run");
        }

        public void ConstructTest(Animator animator, Vector3 moveDirection)
        {
            _animator = animator;

            UpdateAnimation(moveDirection);
        }
    }
}
