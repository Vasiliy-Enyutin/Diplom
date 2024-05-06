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
    }
}
