using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.EnemyLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private GameObject _player;
        private float _pursuitDistance;

        private NavMeshAgent _agent;
        
        public bool IsPursuingPlayer { get; private set; }
        
        public void Init(GameObject player, float moveSpeed, float pursuitDistance)
        {
            _player = player;
            _pursuitDistance = pursuitDistance;
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = moveSpeed;
            _agent.enabled = true;
            IsPursuingPlayer = false;
        }

        private void Update()
        {
            if (_agent.enabled == false || _player == null)
            {
                IsPursuingPlayer = false;
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
                        IsPursuingPlayer = true;
                        _agent.SetPath(path);
                    }
                    else
                    {
                        IsPursuingPlayer = false;
                    }
                }
            }
        }
    }
}
