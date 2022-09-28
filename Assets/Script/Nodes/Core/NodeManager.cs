using System.Collections;

using UnityEngine;

namespace Assets.Script.Nodes.Core
{
    internal class NodeManager : MonoBehaviour
    {
        public BaseNode[] NodesArray { get; set; }

        private void Awake() => NodesArray = GetComponentsInChildren<BaseNode>();

        public IEnumerator Inicialization()
        {
            foreach (var node in NodesArray)
                yield return node.Initialize();
        }
    }
}
