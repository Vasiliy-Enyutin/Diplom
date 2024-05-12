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
            
			UpdateAnimation(_inputService.MoveDirection, _playerGfxTransform.forward);
		}
		
		private void UpdateAnimation(Vector3 moveDirection, Vector3 facingDirection)
		{
			if (moveDirection == Vector3.zero)
			{
				_animator.Play("Idle");
			}
			else
			{
				Vector3 relativeDirection = Quaternion.Inverse(Quaternion.LookRotation(facingDirection)) * moveDirection;

				if (Mathf.Abs(relativeDirection.x) > Mathf.Abs(relativeDirection.z))
				{
					if (relativeDirection.x > 0.5f)
					{
						_animator.Play("RunRight");
					}
					else if (relativeDirection.x < -0.5f)
					{
						_animator.Play("RunLeft");
					}
				}
				else
				{
					if (relativeDirection.z > 0.5f)
					{
						_animator.Play("RunForward");
					}
					else if (relativeDirection.z < -0.5f)
					{
						_animator.Play("RunBackwards");
					}
				}
			}
		}
	}
}