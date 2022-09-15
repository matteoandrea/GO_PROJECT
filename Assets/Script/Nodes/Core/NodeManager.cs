using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Nodes.Core
{
    public class NodeManager : MonoBehaviour
    {
        public NodeCore[] nodes;

        private void Awake()
        {
            nodes = GetComponentsInChildren<NodeCore>();
        }

        private void Start()
        {
            StartCoroutine(Inicialization());
        }

        private IEnumerator Inicialization()
        {
            foreach (var node in nodes)
            {
                node.CheckAllDirections();
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
