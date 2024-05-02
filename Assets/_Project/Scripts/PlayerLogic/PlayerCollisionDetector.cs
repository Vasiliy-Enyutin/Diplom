using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Finish _))
            {
                _player.CollisionWithFinish();
            }
        }
        
        public void ConstructTest(Collider collider, Player player)
        {
            _player = player;
            OnTriggerEnter(collider);
        }
    }
}
