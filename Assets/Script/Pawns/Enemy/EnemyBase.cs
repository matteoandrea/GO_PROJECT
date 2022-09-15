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

        [SerializeField] protected Vector2 startTargetNodeRef;

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

           
        }

        private void OnTriggerExit(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;

        }
    }
}
