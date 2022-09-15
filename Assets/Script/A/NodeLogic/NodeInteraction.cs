using Assets.Script.A.GridLogic;
using Assets.Script.Manager;
using Assets.Script.Pawns.Core;
using Assets.Script.Pawns.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.A.NodeLogic
{
    public class NodeInteraction : MonoBehaviour
    {
        [SerializeField] private GridProxy _gridProxy = default;
        [SerializeField] private Arrow[] _arrows = new Arrow[4];

        private Pawn _player;
        private List<Pawn> _enemyList = new List<Pawn>();

        public Node Node;
        private List<Arrow> _arrowList = new List<Arrow>();

        private void Awake()
        {
            Node = GetComponent<Node>();
        }

        private void Start() => CalculateDirections();

        public void EnableArrows()
        {
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

        public void AddPlayer(Pawn player)
        {
            _player = player;

            if (_enemyList.Count <= 0) return;

            _player.Attack();
            foreach (var item in _enemyList) { item.Die(); }
            _enemyList.Clear();
        }

        public void RemovePlayer() => _player = null;

        public void AddEnemyToList(Pawn enemy)
        {
            if (_enemyList.Contains(enemy)) return;
            _enemyList.Add(enemy);

            if (_player == null) return;
            enemy.Attack();
            _player.Die();
        }

        public void RemoveEnemyFromList(Pawn enemy)
        {
            if (_enemyList.Contains(enemy)) _enemyList.Remove(enemy);
        }

        private void CalculateDirections()
        {
            var direct = _gridProxy.GetNeighbours(Node);
            var directions = new Node[4];

            foreach (var item in direct)
            {
                if (item.gridX == Node.gridX - 1 &&
                    item.gridY == Node.gridY)
                    directions[0] = item;

                else if (item.gridX == Node.gridX + 1 &&
                    item.gridY == Node.gridY)
                    directions[2] = item;

                else if (item.gridX == Node.gridX &&
                    item.gridY == Node.gridY + 1)
                    directions[1] = item;

                else directions[3] = item;
            }

            for (int i = 0; i < directions.Length; i++)
            {
                if (directions[i] == null || !directions[i].walkable) continue;

                _arrows[i].nodePosition = directions[i].worldPosition;
                _arrowList.Add(_arrows[i]);
            }
        }
    }
}
