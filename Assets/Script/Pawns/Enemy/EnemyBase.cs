using Assets.Script.A.GridLogic;
using Assets.Script.A.NodeLogic;
using Assets.Script.A.PathFindingLogic;
using Assets.Script.Input;
using Assets.Script.Manager;
using Assets.Script.Pawns.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public abstract class EnemyBase : Pawn
    {
        [SerializeField] protected GameManagerProxy _managerProxy = default;
        [SerializeField] protected PathRequestManagerProxy _pathProxy;
        [SerializeField] protected GridProxy _gridProxy;

        [SerializeField] protected Vector2 startTargetNodeRef;

        protected Node _currentNodetarget, _startNodeTarget, _endNodeTarget;
        protected Vector3[] _currentPath;
        protected int _currentWaypoint;

        protected virtual void OnEnable() =>
            _managerProxy.startEnemyTurnEvent += ThinkToAct;

        protected virtual void OnDisable() =>
            _managerProxy.startEnemyTurnEvent -= ThinkToAct;

        private void Start()
        {
            _managerProxy.AddEnemy(this);
            StartCoroutine(Init());
        }

        protected virtual IEnumerator Init()
        {
            yield return new WaitForEndOfFrame();
            _currentNodetarget = _endNodeTarget = _gridProxy.GetNode(startTargetNodeRef);
            _startNodeTarget = currentNode;
        }

        public override void Die()
        {
            base.Die();
            _managerProxy.RemoveEnemy(this);
        }

        private void ThinkToAct() => StartCoroutine(Act());

        protected abstract IEnumerator Act();

        protected void CalculatePath(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful) _currentPath = newPath;
        }

        private void OnTriggerEnter(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;

            _nodeInteraction = hit.GetComponent<NodeInteraction>();
            _nodeInteraction.AddEnemyToList(this);
            currentNode = _nodeInteraction.Node;
        }

        private void OnTriggerExit(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;

            _nodeInteraction = hit.GetComponent<NodeInteraction>();
            _nodeInteraction.RemoveEnemyFromList(this);
            currentNode = null;
        }
    }
}
