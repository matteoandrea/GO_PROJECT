using Assets.Script.A.GridLogic;
using Assets.Script.Manager;
using Assets.Script.Pawns.Core;
using System.Collections;
using UnityEngine;

namespace Assets.Script.A.NodeLogic
{
    public class NodeWin : MonoBehaviour
    {
        [SerializeField] GridProxy _gridProxy;
        [SerializeField] GameManagerProxy _managerProxy;
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
            var player = hit.GetComponent<Pawn>();

            if (player == null || player.PawnType != PawnType.Player) return;
            _managerProxy.GameWon = true;
        }

    }
}