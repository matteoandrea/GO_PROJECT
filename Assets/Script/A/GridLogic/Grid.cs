using Assets.Script.A.NodeLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.A.GridLogic
{
    public class Grid : MonoBehaviour, IGrid
    {
        [SerializeField] private GridProxy _gridProxy;
        [SerializeField] private LayerMask _unWalkableMask;
        [SerializeField] private Vector2 _gridWorldSize;
        [SerializeField] private float _nodeDistance;
        [SerializeField] private GameObject _nodePrefab;

        private Node[,] _grid;
        [SerializeField] private int _gridSizeX, _gridSizeY;

        public int MaxSize
        {
            get { return _gridSizeX * _gridSizeY; }
        }

        private void Awake()
        {
            CreateGrid();
            _gridProxy.Grid = this;
        }

        private void CreateGrid()
        {
            _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDistance);
            _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDistance);

            _grid = new Node[_gridSizeX, _gridSizeY];
            var worldBottomLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.forward * _gridWorldSize.y / 2;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    var worldPoint = new Vector3(worldBottomLeft.x + _nodeDistance * x, worldBottomLeft.y, worldBottomLeft.z + _nodeDistance * y);
                    //var worldPoint = (worldBottomLeft + Vector3.right * (x * _nodeDistance) + Vector3.forward * (y * _nodeDistance));
                    var nodeObj = Instantiate(_nodePrefab, worldPoint, Quaternion.identity);
                    nodeObj.transform.SetParent(this.transform);

                    var node = nodeObj.GetComponent<Node>();
                    node.Init(_unWalkableMask, worldPoint, x, y);
                    _grid[x, y] = node;
                }

            }
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            //Debug.Log($"world Position: {worldPosition}");
            var percentX = (worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x;
            var percentY = (worldPosition.z + _gridWorldSize.y / 2) / _gridWorldSize.y;

            //Debug.Log($"Percentage X: {percentX}");
            //Debug.Log($"Percentage Y: {percentY}");

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            //Debug.Log($"Percentage X After Clamp: {percentX}");
            //Debug.Log($"Percentage Y After Clamp: {percentY}");

            var x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
            var y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

            Debug.Log($" X: {x}");
            Debug.Log($" Y: {y}");

            return _grid[x, y];
        }

        public Node GetNode(Vector2 pos)
        { return _grid[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y)]; }

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

                    if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                    {
                        neighbours.Add(_grid[checkX, checkY]);
                    }
                }
            }
            return neighbours;
        }

        public void SwapGrid(Node node) => _grid[node.gridX, node.gridY] = node;
    }
}
