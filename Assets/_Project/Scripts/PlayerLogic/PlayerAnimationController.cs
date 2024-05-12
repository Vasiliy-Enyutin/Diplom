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

		private InputService _inputService;
		private PlayerAttackController _attackController;

		private void Start()
		{
			_inputService = GetComponent<Player>().InputService;
			_attackController = GetComponent<PlayerAttackController>();
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
			string prefix = _attackController.CurrentWeaponType == WeaponType.Melee ? "Melee" : "Ranged";

			if (moveDirection == Vector3.zero)
			{
				_animator.Play(prefix + "Idle");
			}
			else
			{
				Vector3 relativeDirection = Quaternion.Inverse(Quaternion.LookRotation(facingDirection)) * moveDirection;

				if (Mathf.Abs(relativeDirection.x) > Mathf.Abs(relativeDirection.z))
				{
					if (relativeDirection.x > 0.5f)
					{
						_animator.Play(prefix + "RunRight");
					}
					else if (relativeDirection.x < -0.5f)
					{
						_animator.Play(prefix + "RunLeft");
					}
				}
				else
				{
					if (relativeDirection.z > 0.5f)
					{
						_animator.Play(prefix + "RunForward");
					}
					else if (relativeDirection.z < -0.5f)
					{
						_animator.Play(prefix + "RunBackwards");
					}
				}
			}
		}
	}
}