using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null!;
        [SerializeField]
        private AnimationLifetimeHelper _animationLifetimeHelper;
        
        private NavMeshAgent _agent = null!;
        private EnemyAttackController _attackController = null!;
        private bool _isAttacking;

        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        private static readonly int IsIdle = Animator.StringToHash("IsIdle");
        
        private static readonly int AttackTrigger = Animator.StringToHash("OnAttackTrigger");

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _attackController = GetComponent<EnemyAttackController>();

            _animationLifetimeHelper.OnAttackAnimationEnd += OnAttackAnimationEnd;
            _attackController.OnAttacking += HandleAttacking;
        }

        private void Update()
        {
	        if (_agent == null)
	        {
		        return;
	        }

	        if (!_isAttacking)
	        {
		        UpdateAnimation(_agent.velocity);
	        }
        }

        private void UpdateAnimation(Vector3 agentVelocity)
        {
	        if (agentVelocity.magnitude <= Mathf.Epsilon)
	        {
		        _animator.SetBool(IsRunning, false);
		        _animator.SetBool(IsIdle, true);
		        _animator.SetBool(IsAttacking, false);
	        }
	        else
	        {
		        _animator.SetBool(IsRunning, true);
		        _animator.SetBool(IsIdle, false);
		        _animator.SetBool(IsAttacking, false);
	        }
        }

        private void HandleAttacking()
        {
	        _isAttacking = true;
	        _animator.SetBool(IsAttacking, true);
	        _animator.SetTrigger(AttackTrigger);

	        _animator.SetBool(IsRunning, false);
	        _animator.SetBool(IsIdle, false);
        }

        private void OnAttackAnimationEnd()
        {
	        _isAttacking = false;
	        _animator.SetBool(IsAttacking, false);
        }
    }
}
