using _Project.Scripts.PlayerLogic.AttackLogic;
using _Project.Scripts.Services;
using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null!;
        [SerializeField]
        private Transform _playerGfxTransform;
        [SerializeField]
        private AnimationLifetimeHelper _animationLifetimeHelper;

        private InputService _inputService;
        private PlayerAttackController _attackController;

        private string currentWeaponPrefix;
        private bool _isAttacking;

        private void Start()
        {
            _inputService = GetComponent<Player>().InputService;
            _attackController = GetComponent<PlayerAttackController>();

            _animationLifetimeHelper.OnAttackAnimationEnd += OnAttackAnimationEnd;
            _attackController.OnAttacking += HandleAttacking;
        }

        private void OnDestroy()
        {
            if (_attackController != null)
            {
                _attackController.OnAttacking -= HandleAttacking;
            }

            if (_animationLifetimeHelper != null)
            {
	            _animationLifetimeHelper.OnAttackAnimationEnd -= OnAttackAnimationEnd;
            }
        }

        private void Update()
        {
            if (_inputService == null)
            {
                return;
            }

            UpdateAnimation(_inputService.MoveDirection, _playerGfxTransform.forward);
        }

        private void UpdateAnimation(Vector3 moveDirection, Vector3 facingDirection)
        {
	        currentWeaponPrefix = _attackController.CurrentWeaponType == WeaponType.Melee ? "Melee" : "Ranged";

	        if (_isAttacking)
	        {
		        return;
	        }

	        if (moveDirection == Vector3.zero)
            {
                _animator.Play(currentWeaponPrefix + "Idle");
            }
            else
            {
                Vector3 relativeDirection = Quaternion.Inverse(Quaternion.LookRotation(facingDirection)) * moveDirection;

                if (Mathf.Abs(relativeDirection.x) > Mathf.Abs(relativeDirection.z))
                {
                    if (relativeDirection.x > 0.5f)
                    {
                        _animator.Play(currentWeaponPrefix + "RunRight");
                    }
                    else if (relativeDirection.x < -0.5f)
                    {
                        _animator.Play(currentWeaponPrefix + "RunLeft");
                    }
                }
                else
                {
                    if (relativeDirection.z > 0.5f)
                    {
                        _animator.Play(currentWeaponPrefix + "RunForward");
                    }
                    else if (relativeDirection.z < -0.5f)
                    {
                        _animator.Play(currentWeaponPrefix + "RunBackwards");
                    }
                }
            }
        }

        private void HandleAttacking()
        {
            _isAttacking = true;
            _animator.Play(currentWeaponPrefix + "Attack");
        }

        private void OnAttackAnimationEnd()
        {
            _isAttacking = false;
        }
    }
}
