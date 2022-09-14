using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Script.A.GridLogic;
using Assets.Script.A.NodeLogic;
using UnityEngine;
using Grid = Assets.Script.A.GridLogic.Grid;

namespace Assets.Script.A.PathFindingLogic
{
    public class PathFinding : MonoBehaviour
    {
        private Grid _grid;
        private PathRequestManager _pathRequestManager;

        private void Awake()
        {
            _pathRequestManager = GetComponent<PathRequestManager>();
            _grid = GetComponent<Grid>();
        }

        public void StartFindPath(Vector3 startPos, Vector3 endPos)
        {
            StartCoroutine(FindPath(startPos, endPos));
        }

        private IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
        {
            var wayPoints = new Vector3[0];
            var pathSuccess = false;

            var startNode = _grid.NodeFromWorldPoint(startPos);
            var targetNode = _grid.NodeFromWorldPoint(targetPos);

            Debug.Log($"Start: {startNode}");
            Debug.Log($"End: {targetNode}");

            if (startNode.walkable && targetNode.walkable)
            {

                Heap<Node> openSet = new Heap<Node>(_grid.MaxSize);
                HashSet<Node> closeSet = new HashSet<Node>();

                openSet.Add(startNode);

                while (openSet.Count > 0)
                {
                    var currentNode = openSet.RemoveFirst();
                    closeSet.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSuccess = true;
                        break;
                    }

                    foreach (var neighbours in _grid.GetNeighbours(currentNode))
                    {
                        if (!neighbours.walkable || closeSet.Contains(neighbours)) continue;

                        var newMoveCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbours);
                        if (newMoveCostToNeighbour < neighbours.gCost || !openSet.Contains(neighbours))
                        {
                            neighbours.gCost = newMoveCostToNeighbour;
                            neighbours.hCost = GetDistance(neighbours, targetNode);
                            neighbours.parent = currentNode;

                            if (!openSet.Contains(neighbours)) openSet.Add(neighbours);

                        }
                    }

                }
            }
            yield return null;
            if (pathSuccess) wayPoints = RetracePath(startNode, targetNode);

            _pathRequestManager.FinishedProcessPath(wayPoints, pathSuccess);

        }

        private Vector3[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            var currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
           
            path.Reverse();

            var wayPoints = new Vector3[path.Count];
            for (int i = 0; i < wayPoints.Length; i++)
            {
                wayPoints[i] = path[i].worldPosition;
            }

            return wayPoints;
        }

        private int GetDistance(Node nodeA, Node nodeB)
        {
            var distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            var distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            return 10 * distY + 10 * distX;
        }
    }
}