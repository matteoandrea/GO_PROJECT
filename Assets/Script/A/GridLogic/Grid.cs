using Assets.Script.A.NodeLogic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Script.A.GridLogic
{
    public class Grid : MonoBehaviour, IGrid
    {
        [SerializeField] private GridProxy _gridProxy;
        [SerializeField] private LayerMask _unWalkableMask;
        [SerializeField] private Vector2 _gridWorldSize;
        [SerializeField] private float _nodeDistance;
        [SerializeField] private GameObject _nodePrefab;

        private Node[,] grid;
        private int gridSizeX, gridSizeY;

        public int MaxSize
        {
            get { return gridSizeX * gridSizeY; }
        }

        private void Awake()
        {
            CreateGrid();
            _gridProxy.Grid = this;
        }

        private void CreateGrid()
        {
            gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDistance);
            gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDistance);

            grid = new Node[gridSizeX, gridSizeY];
            var worldBottomLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.forward * _gridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    var worldPoint = new Vector3(worldBottomLeft.x + _nodeDistance * x, worldBottomLeft.y, worldBottomLeft.z + _nodeDistance * y);
                    var nodeObj = Instantiate(_nodePrefab, worldPoint, Quaternion.identity);
                    nodeObj.transform.SetParent(this.transform);

                    var node = nodeObj.GetComponent<Node>();
                    node.Init(_unWalkableMask, worldPoint, x, y);
                    grid[x, y] = node;
                }

            }
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            var percentX = (worldPosition.x + gridSizeX / 2) / _gridWorldSize.x;
            var percentY = (worldPosition.z + gridSizeY / 2) / _gridWorldSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            var x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            var y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            return grid[x, y];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0 || x != 0 && y != 0) continue;

                    var checkX = node.gridX + x;
                    var checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1, _gridWorldSize.y));

        }
    }
}
