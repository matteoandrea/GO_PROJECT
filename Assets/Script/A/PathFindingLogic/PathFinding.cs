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

        public void StartFindPath(Node startPos, Node targetPos) => StartCoroutine(FindPath(startPos, targetPos));

        private IEnumerator FindPath(Node startNode, Node targetNode)
        {
            Debug.Log($"start: {startNode}, End: {targetNode}");
            var wayPoints = new Vector3[0];
            var pathSuccess = false;

            //var startNode = _grid.NodeFromWorldPoint(startPos);
            //var targetNode = _grid.NodeFromWorldPoint(targetPos);
            startNode.parent = startNode;

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
                            else openSet.UpdateItem(neighbours);
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
            Debug.Log($"Path Count: {path.Count}");
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

            if (distX > distY) return distY + 10 * (distX - distY);
            else return distX + 10 * (distY - distX);
        }
    }
}