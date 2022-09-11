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

            _managerProxy.startPlayerTurnEvent += CalculatePath;
        }

        private void OnDisable()
        {
            _inputReader.clickEvent -= MoveAction;
            _inputReader.mousePositionEvent -= OnMousePositon;

            _managerProxy.startPlayerTurnEvent -= CalculatePath;
        }

        protected override void Awake()
        {
            _cam = Camera.main;
            base.Awake();
        }

        private void Start()
        {
            PawnType = PawnType.Player;
        }

        private void OnMousePositon(Vector2 pos)
        {
            _mousePosition = pos;
        }

        protected override void MoveAction()
        {
            Ray ray = _cam.ScreenPointToRay(_mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            var direction = hit.collider.GetComponent<Arrow>();

            if (direction == null) return;

            _nodeToMove = direction.PlayerChoose();
            base.MoveAction();
        }

        protected override void CalculatePath()
        {

        }
    }
}
