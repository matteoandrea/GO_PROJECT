using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

namespace Assets.Script.Nodes.Core
{
    public class NodeCore : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        public Dictionary<Directions, NodeCore> conections = new Dictionary<Directions, NodeCore>();

        private Arrow[] _arrowArray;
        private List<Arrow> _connectedArrowList = new List<Arrow>();

        private void Awake()
        {
            _arrowArray = GetComponentsInChildren<Arrow>(true);
        }

        private void Start()
        {
            conections.Add(Directions.Foward, null);
            conections.Add(Directions.Backward, null);
            conections.Add(Directions.Right, null);
            conections.Add(Directions.Left, null);
        }

        public void CheckAllDirections()
        {
            List<Directions> keys = new List<Directions>(conections.Keys);
            _connectedArrowList.Clear();

            foreach (var key in keys)
            {
                CheckDirection(key);
            }
        }

        public void CheckDirection(Directions key)
        {
            var dir = Vector3.zero;
            switch (key)
            {
                case Directions.Foward:
                    dir = Vector3.forward;
                    break;
                case Directions.Backward:
                    dir = Vector3.back;
                    break;
                case Directions.Left:
                    dir = Vector3.left;
                    break;
                case Directions.Right:
                    dir = Vector3.right;
                    break;
            }

            conections.TryGetValue(key, out var value);


            if (Physics.Raycast(transform.position, dir, out var hit, Mathf.Infinity, _layerMask))
            {
                var nodeConnect = hit.transform.GetComponent<NodeCore>();
                if (nodeConnect == null) return;

                conections[key] = nodeConnect;

                foreach (var arrow in _arrowArray)
                {
                    if (arrow.directions == key)
                    {
                        arrow.nodeConnection = nodeConnect;
                        _connectedArrowList.Add(arrow);
                        break;
                    }
                }
            }
        }

        public void EnableArrows()
        {
            foreach (var item in _connectedArrowList)
            {
                item.gameObject.SetActive(true);
            }
        }

        public void DisableArrows()
        {
            foreach (var item in _connectedArrowList)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}