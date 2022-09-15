using UnityEngine;
using Assets.Script.Input;
using Assets.Script.Manager;
using Assets.Script.Pawns.Core;
using Assets.Script.A.NodeLogic;

namespace Assets.Script.Pawns.Player
{
    public class PlayerController : Pawn
    {
        [SerializeField] private InputReader _inputReader = default;
        [SerializeField] private GameManagerProxy _managerProxy = default;

        private Camera _cam;
        private Vector2 _mousePosition;

        private void OnEnable()
        {
            _inputReader.clickEvent += MoveAction;
            _inputReader.mousePositionEvent += OnMousePositon;
            _managerProxy.startPlayerTurnEvent += CheckDirections;
        }

        private void OnDisable()
        {
            _inputReader.clickEvent -= MoveAction;
            _inputReader.mousePositionEvent -= OnMousePositon;
            _managerProxy.startPlayerTurnEvent -= CheckDirections;
        }

        protected override void Awake()
        {
            _cam = Camera.main;
            base.Awake();
        }

        public override void Die()
        {
            base.Die();
            _managerProxy.GameLost = true;
        }

        private void OnMousePositon(Vector2 pos) => _mousePosition = pos;

        private void CheckDirections() => _nodeInteraction.EnableArrows();

        protected override void MoveAction()
        {
            Ray ray = _cam.ScreenPointToRay(_mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            var direction = hit.collider.GetComponent<Arrow>();

            if (direction == null) return;

            _nodeInteraction.DisableArrows();
            _targetPosition = direction.nodePosition;

            base.MoveAction();
        }

        private void OnTriggerEnter(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;

            _nodeInteraction = hit.GetComponent<NodeInteraction>();
            _nodeInteraction.AddPlayer(this);
            currentNode = _nodeInteraction.Node;
        }

        private void OnTriggerExit(Collider hit)
        {
            if (!hit.CompareTag("Node")) return;

            _nodeInteraction = hit.GetComponent<NodeInteraction>();
            _nodeInteraction.RemovePlayer();
            currentNode = null;
        }
    }
}
