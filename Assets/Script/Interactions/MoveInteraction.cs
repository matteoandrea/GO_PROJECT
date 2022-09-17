using Assets.Script.Nodes.core;
using Assets.Script.Nodes.Core;
using Assets.Script.Pawns.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Interactions
{
    public class MoveInteraction : MonoBehaviour, IInteract
    {
        [SerializeField] private Directions _direction;

        private BaseNode _nodeToMove { get; set; }
        private BaseNode _currentNode { get; set; }

        private ArrowAnimation _arrowAnimation { get; set; }
        private LineConnection _lineConnetion { get; set; }


        private void Awake()
        {
            _arrowAnimation =
                GetComponentInChildren<ArrowAnimation>(true);
            _currentNode = GetComponentInParent<BaseNode>(true);
            _lineConnetion = GetComponent<LineConnection>();
        }

        public void SetMoveNode(Dictionary<Directions, BaseNode> connections)
        {
            if (connections.ContainsKey(_direction))
                _nodeToMove = connections[_direction];

            if (_nodeToMove == null) return;

            _lineConnetion.EnableLine();
            var distance = Mathf.Abs(Vector3.Distance(transform.position, _nodeToMove.transform.position)
               ) * .5f;
            StartCoroutine(_lineConnetion.SetLinePath(distance));
        }

        public void SetArrowState(bool state)
        {
            if (_nodeToMove != null)
                _arrowAnimation.gameObject.SetActive(state);
        }

        public void ExecuteInteraction(PlayerController player)
        {
            player.MoveAction(_nodeToMove.transform.position);
            _currentNode.PlayerEndTurn();

        }
    }
}