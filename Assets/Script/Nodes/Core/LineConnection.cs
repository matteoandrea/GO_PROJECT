using Assets.Script.Nodes.Core;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Nodes.core
{
    public class LineConnection : MonoBehaviour
    {
        [SerializeField]
        private Directions _direction;
        [SerializeField]
        private float _lineAnimationSpeed = .35f;

        private LineRenderer _lineRenderer { get; set; }
        private Tween _tween { get; set; }


        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public IEnumerator SetLinePath(float tartgetEnd)
        {
            var startLine = Vector3.zero;
            var endLine = Vector3.zero;

            switch (_direction)
            {
                case Directions.Foward:
                    startLine = new Vector3(0, .1f, 2);
                    endLine = startLine + Vector3.forward * tartgetEnd;
                    break;
                case Directions.Backward:
                    startLine = new Vector3(0, .1f, -2);
                    endLine = startLine + Vector3.back * tartgetEnd;
                    break;
                case Directions.Left:
                    startLine = new Vector3(-2, .1f, 0);
                    endLine = startLine + Vector3.left * tartgetEnd;
                    break;
                case Directions.Right:
                    startLine = new Vector3(2, .1f, 0);
                    endLine = startLine + Vector3.right * tartgetEnd;
                    break;
            }

            _lineRenderer.SetPosition(0, startLine);

            var vectorValue = startLine;
            DOTween.To(() => vectorValue, x => vectorValue
            = x, endLine, _lineAnimationSpeed);

            while (vectorValue != endLine)
            {
                _lineRenderer.SetPosition(1, vectorValue);
                yield return null;
            }
        }

        public void EnableLine() => _lineRenderer.enabled = true;
    }
}