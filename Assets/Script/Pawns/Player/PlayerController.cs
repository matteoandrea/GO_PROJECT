using UnityEngine;
using Assets.Script.Input;
using Assets.Script.Manager;
using Assets.Script.Pawns.Core;
using Assets.Script.Nodes.Core;
using Assets.Script.Command;

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
            _inputReader.clickEvent += ChooseAction;
            _inputReader.mousePositionEvent += OnMousePositon;
            _gameManagerProxy.startPlayerTurnEvent += OnStartTurn;
        }

        private void OnDisable()
        {
            _inputReader.clickEvent -= ChooseAction;
            _inputReader.mousePositionEvent -= OnMousePositon;
            _gameManagerProxy.startPlayerTurnEvent -= OnStartTurn;
        }

        protected override void Awake()
        {
            _cam = Camera.main;
            base.Awake();
        }

        protected override void OnEnterNode(NodeCore node)
        {

        }

        protected override void OnExitNode(NodeCore node)
        {

        }

        protected override void OnStartTurn()
        {
            _currentNode.EnableArrows();
        }

        private void ChooseAction()
        {
            Ray ray = _cam.ScreenPointToRay(_mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, _interactMask)) return;

            var script = hit.collider.GetComponent<Arrow>();
            if (script != null) MoveAction(script);
        }

        private void OnMousePositon(Vector2 pos) => _mousePosition = pos;

        private void MoveAction(Arrow arrow)
        {
            var target = arrow.GetNodeConnection();
            ICommand command = new PlayerMoveCommand(target, transform, _animator);
            _gameManagerProxy.AddCommand(command);
        }

        public override void Die()
        {
            base.Die();
            _gameManagerProxy.GameLost = true;
        }
    }
}
