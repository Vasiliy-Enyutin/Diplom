using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.EnemyLogic
{
    public class EnemyMovement : MonoBehaviour
    {
        private Enemy _enemy;
        private GameObject _player;
        private float _pursuitDistance;

        private NavMeshAgent _agent;
        private float _distanceToPlayer;
        private Transform _targetTransform;

        public bool EnemyCanAttack { get; private set; } = false;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            _player = _enemy.Target;
            _pursuitDistance = _enemy.EnemyDescriptor.PursuitDistance;

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _enemy.EnemyDescriptor.MoveSpeed;
        }

        private void Update()
        {
            RotateTowardsPlayer();

            if (_agent.enabled == false)
            {
                _enemy.IsPursuingPlayer = false;
                return;
            }

            UpdatePath();
            CheckForCanAttack();
        }

        public void AgentSetActive(bool active)
        {
            _agent.enabled = active;
        }

        private void CheckForCanAttack()
        {
	        if (_agent.remainingDistance <= _agent.stoppingDistance)
	        {
		        if (_distanceToPlayer < _pursuitDistance || _enemy.IsPursuingPlayer == false)
		        {
			        EnemyCanAttack = true;
			        return;
		        }
	        }

	        EnemyCanAttack = false;
        }

        private void UpdatePath()
        {
            NavMeshPath path = new();

            if (_player != null && _agent.CalculatePath(_player.transform.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {
	            _distanceToPlayer = CalculatePathDistance(path);

                if (_distanceToPlayer <= _pursuitDistance)
                {
                    _targetTransform = _player.transform;
                    _enemy.IsPursuingPlayer = true;
                }
                else
                {
                    SetTargetToMainBuilding();
                }
            }
            else
            {
                SetTargetToMainBuilding();
            }

            if (_agent.CalculatePath(_targetTransform.position, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                _agent.SetPath(path);
            }
        }

        private void SetTargetToMainBuilding()
        {
	        if (_enemy.MainBuilding == null)
	        {
		        return;
	        }
            _enemy.IsPursuingPlayer = false;
            _targetTransform = _enemy.MainBuilding.transform;
            _agent.SetDestination(_targetTransform.position);
        }

        private float CalculatePathDistance(NavMeshPath path)
        {
            float distance = 0f;
            Vector3[] corners = path.corners;
            for (int i = 0; i < corners.Length - 1; ++i)
            {
                distance += Vector3.Distance(corners[i], corners[i + 1]);
            }
            return distance;
        }

        private void RotateTowardsPlayer()
        {
	        if (_targetTransform == null)
	        {
		        return;
	        }
	        
	        if (EnemyCanAttack)
            {
                Vector3 direction = (_targetTransform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _agent.angularSpeed);
            }
        }
    }
}