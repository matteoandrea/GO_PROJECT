using Assets.Script.Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Script.Nodes.Core
{
    public class Arrow : MonoBehaviour
    {
        public Directions directions;
        private NodeCore _nodeCore;
        public NodeCore nodeConnection;

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

        private void Awake()
        {
            _nodeCore = gameObject.GetComponentInParent<NodeCore>();   
        }

        public Vector3 GetNodeConnection()
        {
            _nodeCore.DisableArrows();  
            return nodeConnection.transform.position;
        }

    }
}