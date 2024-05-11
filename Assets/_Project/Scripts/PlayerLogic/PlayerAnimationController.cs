using _Project.Scripts.Services;
using UnityEngine;

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
			if (moveDirection == Vector3.zero)
			{
				_animator.Play("Idle");
			}
			else if (moveDirection.x > 0 || (moveDirection.x > 0 && moveDirection.y > 0) || (moveDirection.x > 0 && moveDirection.y < 0))
			{
				_animator.Play("RunRight");
			}
			else if (moveDirection.x < 0 || (moveDirection.x < 0 && moveDirection.y > 0) || (moveDirection.x < 0 && moveDirection.y < 0))
			{
				_animator.Play("RunLeft");
			}
			else if (moveDirection.z < 0)
			{
				_animator.Play("RunBackwards");
			}
			else
			{
				_animator.Play("RunForward");
			}
		}
	}
}