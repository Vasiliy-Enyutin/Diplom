using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null!;
        
        private NavMeshAgent _agent = null!;

        private void Awake()
        {
	        _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_agent == null)
            {
                return;
            }
            
            UpdateAnimation(_agent.velocity);
        }

        private void UpdateAnimation(Vector3 agentVelocity)
        {
            _animator.Play(agentVelocity == Vector3.zero ? "Idle" : "Run");
        }
    }
}
