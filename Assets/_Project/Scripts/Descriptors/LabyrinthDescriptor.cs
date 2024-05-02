using _Project.Scripts.LabyrinthLogic;
using UnityEngine;

namespace _Project.Scripts.Descriptors
{
    [CreateAssetMenu(fileName = "LabyrinthDescriptor", menuName = "Descriptors/Labyrinth", order = 0)]
    public class LabyrinthDescriptor : ScriptableObject
    {
        public Cell CellPrefab;
        public Vector3 CellSize;
        public int LabyrinthWidth;
        public int LabyrinthHeight;
    }
}