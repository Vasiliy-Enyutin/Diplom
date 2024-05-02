using UnityEngine;
using UnityEngine.AI;

namespace _Project.NavMeshComponents
{
    public class NavigationBaker : MonoBehaviour 
    {
        private void Start()
        {
            FindObjectOfType<NavMeshSurface>().BuildNavMesh();
        }
    }
}