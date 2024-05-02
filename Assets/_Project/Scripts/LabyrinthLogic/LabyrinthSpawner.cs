using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.LabyrinthLogic
{
    public class LabyrinthSpawner : MonoBehaviour
    {
        [SerializeField]
        private Material _finishMaterial;
        
        [Inject]
        private AssetProviderService _assetProviderService = null!;

        private const float FINISH_COLLIDER_RADIUS = 2;
        
        private Cell _cell;
        private Vector3 _cellSize;
        private int _labyrinthWidth;
        private int _labyrinthHeight;
        
        private Labyrinth _labyrinth;

        public void Init(Cell cell, Vector3 cellSize, int labyrinthWidth, int labyrinthHeight)
        {
            _cell = cell;
            _cellSize = cellSize;
            _labyrinthWidth = labyrinthWidth;
            _labyrinthHeight = labyrinthHeight;
            
            GenerateLabyrinth();
        }
        
        private void GenerateLabyrinth()
        {
            LabyrinthGenerator generator = new();
            _labyrinth = generator.GenerateMaze(_labyrinthWidth, _labyrinthHeight);

            for (int x = 0; x < _labyrinth.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < _labyrinth.Cells.GetLength(1); y++)
                {
                    Cell c = _assetProviderService.CreateAsset<Cell>(_cell, new Vector3(x * _cellSize.x, y * _cellSize.y, y * _cellSize.z));

                    c.transform.SetParent(transform);
                    c.WallLeft.SetActive(_labyrinth.Cells[x, y].WallLeft);
                    c.WallBottom.SetActive(_labyrinth.Cells[x, y].WallBottom);

                    if (x == _labyrinth.FinishPosition.x && y == _labyrinth.FinishPosition.y)
                    {
                        AddColliderToFinish(c);
                    }
                }
            }
        }

        private void AddColliderToFinish(Cell cell)
        {
            cell.AddComponent<Finish>();
            SphereCollider sphereCollider = cell.AddComponent<SphereCollider>();
            sphereCollider.radius = FINISH_COLLIDER_RADIUS;
            sphereCollider.isTrigger = true;            
            cell.Floor.GetComponent<MeshRenderer>().material = _finishMaterial;
        }
    }
}