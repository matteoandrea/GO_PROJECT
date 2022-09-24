using Assets.Script.Manager;
using DG.Tweening;
using UnityEngine;

namespace Assets.Script.A.NodeLogic
{
    public class Arrow : MonoBehaviour
    {
        [HideInInspector] public Vector3 nodePosition;

        [SerializeField] private Vector3 _pos;
        private Tween _tween;
        private Vector3 _startPos;

        private void OnEnable()
        {
            _startPos = transform.position;
            _tween = transform.DOMove(transform.position + _pos, .5f)
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