using UnityEngine;

namespace _Project.Scripts.LabyrinthLogic
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private GameObject _wallLeft;
        [SerializeField] private GameObject _wallBottom;
        [SerializeField] private GameObject _floor;

        public GameObject WallLeft => _wallLeft;
        public GameObject WallBottom => _wallBottom;
        public GameObject Floor => _floor;
    }
}