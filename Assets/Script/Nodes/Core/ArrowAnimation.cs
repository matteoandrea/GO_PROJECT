using Assets.Script.Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Nodes.Core
{
    public class ArrowAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 _pos;
        [SerializeField] private float _speedAnimation;
        private Tween _tween { get; set; }
        private Vector3 _startPos { get; set; }

        private void OnEnable()
        {
            _startPos = transform.position;
            _tween = transform.DOMove(transform.position + _pos, _speedAnimation)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDisable()
        {
            _tween.Kill();
            transform.position = _startPos;
        }
    }
}