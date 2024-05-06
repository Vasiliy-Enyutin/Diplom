using UnityEngine;

namespace _Project.Scripts.PlayerLogic
{
    public class PlayerInputService : MonoBehaviour
    {
        public Vector3 MoveDirection
        {
            get { return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")); }
        }
    }
}
