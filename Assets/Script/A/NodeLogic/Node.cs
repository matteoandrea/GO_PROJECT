using Assets.Script.A.GridLogic;
using Assets.Script.Pawns.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.A.NodeLogic
{
    public class Node : MonoBehaviour, IHeapItem<Node>
    {
        public bool walkable;
        public Vector3 worldPosition;

        public int gCost, hCost, gridX, gridY;
        public Node parent;

        private float _radius;
        private int _heapIndex;

        public int fCost { get { return gCost + hCost; } }

        public int HeapIndex
        {
            get { return _heapIndex; }
            set { _heapIndex = value; }
        }

        private void Awake()
        {
            _radius = GetComponent<SphereCollider>().radius;
        }

        public void Init(LayerMask mask, Vector3 worldPosition, int gridX, int gridY)
        {
            this.worldPosition = worldPosition;
            this.gridX = gridX;
            this.gridY = gridY;
            name = $"{gridX.ToString()},{gridY.ToString()}";

            walkable = !(Physics.CheckSphere(transform.position, _radius, mask));
        }

        public int CompareTo(Node nodeToCompare)
        {
            var compare = fCost.CompareTo(nodeToCompare.fCost);

            if (compare == 0) compare = hCost.CompareTo(nodeToCompare.hCost);

            return -compare;
        }
    }
}
