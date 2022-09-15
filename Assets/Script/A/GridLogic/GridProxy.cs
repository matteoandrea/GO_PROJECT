using Assets.Script.A.NodeLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.A.GridLogic
{
    [CreateAssetMenu(fileName = "GridProxy", menuName = "Managers/Grid Proxy Manager Proxy")]
    public class GridProxy : ScriptableObject, IGrid
    {
        public Grid Grid { private get; set; }

        public List<Node> GetNeighbours(Node node) => Grid.GetNeighbours(node);
        public void SwapGrid(Node node) => Grid.SwapGrid(node);
        public Node GetNode(Vector2 pos) => Grid.GetNode(pos);
    }
}