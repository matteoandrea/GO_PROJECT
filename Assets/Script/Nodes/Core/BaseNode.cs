using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Interactions;
using System.Collections;
using Assets.Script.Pawns.Core;
using Assets.Script.Nodes.core;

namespace Assets.Script.Nodes.Core
{
    public class BaseNode : MonoBehaviour
    {
        public Dictionary<Directions, BaseNode> Conections = new();
        private Pawn _player;
        public Pawn Player
        {
            get { return _player; }
            set
            {
                _player = value;

                if (_enemiesList.Count > 0 && value != null)
                {
                    _player.Attack();
                    foreach (var enemy in _enemiesList)
                        enemy.Die();

                    _enemiesList.Clear();
                }
            }
        }
        private List<Pawn> _enemiesList = new();

        [SerializeField] private LayerMask _incluedlayerMask;
        private MoveInteraction[] _allMoveInteraction { get; set; }

        private void Awake()
        {
            _allMoveInteraction = GetComponentsInChildren<MoveInteraction>(true);
        }

        public IEnumerator Initialize()
        {
            Conections.Add(Directions.Foward, null);
            Conections.Add(Directions.Backward, null);
            Conections.Add(Directions.Left, null);
            Conections.Add(Directions.Right, null);

            yield return StartCoroutine
                (CheckAllDirections());
            yield return StartCoroutine(SetInteractionConection());
        }

        private IEnumerator CheckAllDirections()
        {
            List<Directions> keys = new(Conections.Keys);
            foreach (var key in keys)
            {
                yield return StartCoroutine(CheckDirection(key));
            }
        }

        private IEnumerator CheckDirection(Directions key)
        {
            var direction = ChooseDirection(key);

            if (Physics.Raycast(transform.position,
                direction, out var hit, Mathf.Infinity, _incluedlayerMask))
            {
                if (!hit.transform.TryGetComponent<BaseNode>
                    (out var connectedNode)) yield break;

                Conections[key] = connectedNode;
            }
        }

        private IEnumerator SetInteractionConection()
        {
            foreach (MoveInteraction interaction in _allMoveInteraction)
            {
                interaction.SetMoveNode(Conections);
                yield return null;
            }
        }

        private Vector3 ChooseDirection(Directions key)
        {
            var direction = Vector3.zero;
            switch (key)
            {
                case Directions.Foward:
                    direction = Vector3.forward;
                    break;
                case Directions.Backward:
                    direction = Vector3.back;
                    break;
                case Directions.Left:
                    direction = Vector3.left;
                    break;
                case Directions.Right:
                    direction = Vector3.right;
                    break;
            }

            return direction;
        }

        public void PlayerStartTurn()
        {
            foreach (var moveInteraction in _allMoveInteraction)
            {
                moveInteraction.SetArrowState(true);
            }
        }

        public void PlayerEndTurn()
        {
            foreach (var moveInteraction in _allMoveInteraction)
            {
                moveInteraction.SetArrowState(false);
            }
        }

        public void AddEnemy(Pawn enemy)
        {
            _enemiesList.Add(enemy);
            if (Player != null)
            {
                Player.Die();
                enemy.Attack();
            }
        }

        public void RemoveEnemy(Pawn enemy)
        {
            if (_enemiesList.Contains(enemy)) _enemiesList.Remove(enemy);
        }

    }
}