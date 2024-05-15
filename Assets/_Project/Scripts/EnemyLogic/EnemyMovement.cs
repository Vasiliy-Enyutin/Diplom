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
			if (_agent.enabled == false || _player == null)
			{
				EnemyCanAttack = false;
				_enemy.IsPursuingPlayer = false;
				return;
			}

			UpdatePath();
			CheckForCanAttack();
			RotateTowardsPlayer();
		}

		private void CheckForCanAttack()
		{
			if (_agent.remainingDistance <= _agent.stoppingDistance && _distanceToPlayer < _pursuitDistance)
			{
				EnemyCanAttack = true;
			}
			else
			{
				EnemyCanAttack = false;
			}
		}

		private void UpdatePath()
		{
			NavMeshPath path = new();
			if (_agent.CalculatePath(_player.transform.position, path))
			{
				if (path.status == NavMeshPathStatus.PathComplete)
				{
					_distanceToPlayer = 0f;
					Vector3[] corners = path.corners;
					for (int i = 0; i < corners.Length - 1; ++i)
					{
						_distanceToPlayer += Vector3.Distance(corners[i], corners[i + 1]);
					}

					if (_distanceToPlayer < _pursuitDistance)
					{
						_enemy.IsPursuingPlayer = true;
						_agent.SetPath(path);
					}
					else
					{
						_enemy.IsPursuingPlayer = false;
					}
				}
			}
		}
		
		private void RotateTowardsPlayer()
		{
			if (EnemyCanAttack)
			{
				Vector3 direction = (_player.transform.position - transform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
				transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _agent.angularSpeed);
			}
		}
	}
}