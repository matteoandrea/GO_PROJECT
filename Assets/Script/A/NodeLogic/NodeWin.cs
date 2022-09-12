using Assets.Script.A.GridLogic;
using System.Collections;
using UnityEngine;

namespace Assets.Script.A.NodeLogic
{
    public class NodeWin : MonoBehaviour
    {
        [SerializeField] GridProxy _gridProxy;
        private Node _node;

        private void Awake()
        {
            _node = GetComponent<Node>();
        }

        private void Start()
        {
            _node.InitManual();
            _gridProxy.SwapGrid(_node);
        }

        private void OnTriggerEnter(Collider hit)
        {
            
        }

    }
}