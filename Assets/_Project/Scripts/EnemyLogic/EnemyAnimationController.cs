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
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        private static readonly int RunHash = Animator.StringToHash("Run");
        private static readonly int IdleHash = Animator.StringToHash("Idle");

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _attackController = GetComponent<EnemyAttackController>();

            _animationLifetimeHelper.OnAttackAnimationEnd += OnAttackAnimationEnd;
            _attackController.OnAttacking += HandleAttacking;
            
            if (_agent == null)
            {
	            return;
            }

            UpdateAnimation(_agent.velocity);
        }

        // private void Update()
        // {
        //     if (_agent == null)
        //     {
        //         return;
        //     }
        //
        //     UpdateAnimation(_agent.velocity);
        // }

        private void UpdateAnimation(Vector3 agentVelocity)
        {
            if (_isAttacking)
            {
                return;
            }

            if (agentVelocity == Vector3.zero)
            {
                _animator.CrossFade(IdleHash, 0.1f);
            }
            else
            {
                _animator.CrossFade(RunHash, 0.1f);
            }
        }

        private void HandleAttacking()
        {
            _isAttacking = true;
            _animator.CrossFade(AttackHash, 0.1f);
        }

        private void OnAttackAnimationEnd()
        {
            _isAttacking = false;
            UpdateAnimation(_agent.velocity);
        }
    }
}
