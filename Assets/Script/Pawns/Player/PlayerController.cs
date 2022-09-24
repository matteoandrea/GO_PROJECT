using UnityEngine;
using Assets.Script.Input;
using Assets.Script.Pawns.Core;
using Assets.Script.Nodes.Core;
using Assets.Script.Commands;
using Assets.Script.Interactions;

namespace Assets.Script.Pawns.Player
{
    public class PlayerController : Pawn
    {
        [SerializeField] private InputReader _inputReader = default;
        [SerializeField] private LayerMask _interactMask;

        private Camera _cam;
        private Vector2 _mousePosition;

        private void OnEnable()
        {
            _inputReader.clickEvent += Interact;
            _inputReader.mousePositionEvent += OnMousePositon;
            _gameManagerProxy.startPlayerTurnEvent += OnStartTurn;
        }

        private void OnDisable()
        {
            _inputReader.clickEvent -= Interact;
            _inputReader.mousePositionEvent -= OnMousePositon;
            _gameManagerProxy.startPlayerTurnEvent -= OnStartTurn;
        }

        protected override void Awake()
        {
            _cam = Camera.main;
            base.Awake();
        }

        protected override void OnEnterNode(BaseNode node)
            => node.Player = this;

        protected override void OnExitNode(BaseNode node)
            => node.Player = null;

        protected override void OnStartTurn()
            => _currentNode.PlayerStartTurn();

        private void Interact()
        {
            Ray ray = _cam.ScreenPointToRay(_mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, _interactMask)) return;

            var script = hit.collider.GetComponent<IInteract>();
            if (script != null) script.ExecuteInteraction(this);
        }

        private void OnMousePositon(Vector2 pos)
            => _mousePosition = pos;

        public override void MoveAction(Vector3 targetPosition)
        {
            ICommand command = new PlayerMoveCommand(
                targetPosition,
                _walkSpeed,
                _rotationSpeed,
                transform,
                _animator);
            _gameManagerProxy.AddCommand(command);
        }

        public override void Die()
        {
            base.Die();
            _gameManagerProxy.GameLost = true;
        }
    }
}
