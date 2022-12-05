using Assets.Script.Commands;
using Assets.Script.Commands.Core;
using Assets.Script.Nodes.Core;
using Assets.Script.Pawns.Core;
using DG.Tweening;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Script.Pawns.Enemy
{
    public abstract class EnemyBase : Pawn
    {
        [SerializeField]
        protected LayerMask nodeLayerMask;

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

        protected (BaseNode, Vector3) IsPawnInSight(LayerMask layerMask)
        {
            RaycastHit[] hits;

            hits = Physics.RaycastAll(transform.position, transform.forward, Mathf.Infinity, layerMask);

            float[] dists = CalculateArrayDist(hits);
            SortAllArrays(dists, hits);
            var endPoint = GetEndPoint(hits);

            return (endPoint, endPoint.transform.position);
        }

        private float[] CalculateArrayDist(RaycastHit[] hits)
        {
            float[] dists = new float[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                dists[i] = MathF.Abs(Vector3.Distance(transform.position, hits[i].transform.position));
            }

            return dists;
        }

        private void SortAllArrays(float[] arrayToSort, RaycastHit[] hits)
        {
            for (int i = 0; i < arrayToSort.Length - 1; i++)
            {
                int smallIndex = i;
                for (int j = 0; j < arrayToSort.Length; j++)
                {
                    if (arrayToSort[j] < arrayToSort[smallIndex])
                        smallIndex = j;
                }
                if (smallIndex != i)
                {
                    ChangeArrayPosition(arrayToSort, i, smallIndex);
                    ChangeArrayPosition(hits, i, smallIndex);
                }
            }
        }

        private BaseNode GetEndPoint(RaycastHit[] hits)
        {
            BaseNode endPoint = null;

            for (int i = hits.Length; i-- > 0;)
            {
                BaseNode node = hits[i].transform.GetComponent<BaseNode>();

                if (node == null) break;

                if (!node.IsAnyPawnInside())
                    continue;
                else
                    endPoint = node;
                break;
            }

            return endPoint;
        }

        private void ChangeArrayPosition<T>(T[] array, int originalIndex, int newIndex)
        {
            var temp = array[originalIndex];
            array[originalIndex] = array[newIndex];
            array[newIndex] = temp;
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
