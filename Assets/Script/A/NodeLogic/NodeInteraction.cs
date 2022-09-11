using Assets.Script.A.GridLogic;
using Assets.Script.Manager;
using Assets.Script.Pawns.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.A.NodeLogic
{
    public class NodeInteraction : MonoBehaviour
    {
        [SerializeField] private GameManagerProxy _managerProxy = default;
        [SerializeField] private GridProxy _gridProxy = default;
        [SerializeField] private Arrow[] _arrows = new Arrow[4];

        private Pawn _player;
        private List<Pawn> _enemyList;

        private Node _node;
        private List<Arrow> _arrowList = new List<Arrow>();

        private void OnEnable()
        {
            _managerProxy.startPlayerTurnEvent += CheckPlayer;
        }

        private void OnDisable()
        {
            _managerProxy.startPlayerTurnEvent -= CheckPlayer;
        }

        private void Awake()
        {
            _node = GetComponent<Node>();
        }

        private void Start()
        {
            CalculateDirections();
        }

        private void CheckPlayer()
        {
            if (_player == null) return;

            foreach (var item in _arrowList)
            {
                item.gameObject.SetActive(true);
            }
        }

        public void DisableArrows()
        {
            foreach (var item in _arrowList)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void CalculateDirections()
        {
            var direct = _gridProxy.GetNeighbours(_node);
            var directions = new Node[4];

            foreach (var item in direct)
            {
                if (item.gridX == _node.gridX - 1 &&
                    item.gridY == _node.gridY)
                    directions[0] = item;

                else if (item.gridX == _node.gridX + 1 &&
                    item.gridY == _node.gridY)
                    directions[2] = item;

                else if (item.gridX == _node.gridX &&
                    item.gridY == _node.gridY + 1)
                    directions[1] = item;

                else directions[3] = item;
            }

            for (int i = 0; i < directions.Length; i++)
            {
                if (directions[i] == null || !directions[i].walkable) continue;

                _arrows[i].node = directions[i];
                _arrows[i].nodeInteraction = this;
                _arrowList.Add(_arrows[i]);
            }
        }

        private void OnTriggerEnter(Collider hit)
        {
            var pawn = hit?.GetComponent<Pawn>();
            if (pawn == null) return;

            pawn.currentNode = _node;

            if (pawn.PawnType == PawnType.Player) _player = pawn;
            else _enemyList.Add(pawn);
        }

        private void OnTriggerExit(Collider hit)
        {
            var pawn = hit?.GetComponent<Pawn>();
            if (pawn == null) return;

            if (pawn.PawnType == PawnType.Player) _player = null;
            else if (_enemyList.Contains(pawn)) _enemyList.Remove(pawn);
        }
    }
}
