using Assets.Script.Commands;
using Assets.Script.Commands.Core;
using Assets.Script.Nodes.Core;
using Assets.Script.Pawns.Core;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Script.Pawns.Enemy
{
    public abstract class EnemyBase : Pawn
    {
        [SerializeField]
        protected LayerMask nodeLayerMask, pawLayerMask;

        protected virtual void OnEnable() => gameManagerProxy.startEnemyTurnEvent += OnStartTurn;

        protected virtual void OnDisable() => gameManagerProxy.startEnemyTurnEvent -= OnStartTurn;

        private void Start() => gameManagerProxy.AddEnemy(this);

        protected void Rotate()
        {
            ICommand command = new RotateCommand(
                transform,
                rotationSpeed);

            commandPlayList.AddCommand(command);
        }

        protected void VerifyRotate()
        {
            ICommand command = new VerifyRotateCommand(
                transform,
                rotationSpeed,
                nodeLayerMask);

            commandPlayList.AddCommand(command);
        }

        protected void Pass()
        {
            ICommand command = new PassCommand();
            commandPlayList.AddCommand(command);

        }

        protected (bool, BaseNode, Vector3) IsPawnInSight(LayerMask layerMask)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity,
                                layerMask))
            {
                var obj = hit.transform.gameObject;
                var pawn = obj.GetComponent<Pawn>();

                if (pawn != null)
                    return (true, pawn.currentNode, hit.transform.position);
                else
                    return (false, null, Vector3.one * 1000);
            }
            else
                return (false, null, Vector3.one * 1000);
        }

        protected (bool, Vector3) IsNodeInSight()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity,
                                 nodeLayerMask))
            {
                var script = hit.collider.GetComponent<BaseNode>();

                if (script != null)
                    return (true, hit.transform.position);
                else
                    return (false, Vector3.zero);
            }
            else
                return (false, Vector3.zero);
        }

        protected (bool, Vector3) IsPLayerNear()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity,
                                 nodeLayerMask))
            {
                var script = hit.collider.GetComponent<BaseNode>();

                if (script != null)
                {
                    if (script.Player != null)
                        return (true, hit.transform.position);
                    else
                        return (false, Vector3.zero);
                }
                else
                    return (false, Vector3.zero);
            }
            else
                return (false, Vector3.zero);
        }

        protected override void OnEnterNode(BaseNode node) => node.AddEnemy(this);

        protected override void OnExitNode(BaseNode node) => node.RemoveEnemy(this);

        public override IEnumerator Die()
        {
            gameManagerProxy.startEnemyTurnEvent -= OnStartTurn;
            gameManagerProxy.RemoveEnemy(this);

            yield return base.Die();

            var sequence = DOTween.Sequence();
            sequence
                .Join(transform.DOMoveY(transform.position.y - 20, 1))
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });

            yield break;
        }
    }
}
