using Assets.Script.A.NodeLogic;
using Assets.Script.Manager;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Script.A.GridLogic
{
    public interface IGrid
    {
        public List<Node> GetNeighbours(Node node);
        public void SwapGrid(Node node);
        public Node GetNode(Vector2 pos);
    }
}