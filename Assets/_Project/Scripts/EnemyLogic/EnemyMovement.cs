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
				_enemy.IsPursuingPlayer = false;
				return;
			}

			UpdatePath();
		}

		private void UpdatePath()
		{
			NavMeshPath path = new();
			if (_agent.CalculatePath(_player.transform.position, path))
			{
				if (path.status == NavMeshPathStatus.PathComplete)
				{
					float distanceToPlayer = 0;
					Vector3[] corners = path.corners;
					for (int i = 0; i < corners.Length - 1; ++i)
					{
						distanceToPlayer += Vector3.Distance(corners[i], corners[i + 1]);
					}

					if (distanceToPlayer < _pursuitDistance)
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
	}
}