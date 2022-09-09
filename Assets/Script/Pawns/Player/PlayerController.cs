using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Assets.Script.Input;
using Assets.Script.Events;
using Assets.Script.Manager;
using Assets.Script.Pawns;
using Assets.Script.Command;

namespace Assets.Script.Pawns.Player
{
    public class PlayerController : Pawn
    {
        [SerializeField] private InputReader _inputReader = default;

        private Vector2 _mousePosition;

        private void OnEnable()
        {
            _inputReader.clickEvent += MoveAction;
            _inputReader.mousePositionEvent += OnMousePositon;
        }

        private void OnDisable()
        {
            _inputReader.clickEvent -= MoveAction;
            _inputReader.mousePositionEvent -= OnMousePositon;
        }

        private void OnMousePositon(Vector2 pos)
        {
            _mousePosition = pos;
        }

        protected override void MoveAction()
        {
            Ray ray = _cam.ScreenPointToRay(_mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            _targetPosition = hit.point;
            base.MoveAction();
        }
    }
}
