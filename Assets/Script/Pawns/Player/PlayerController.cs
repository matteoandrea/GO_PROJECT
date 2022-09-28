using UnityEngine;
using Assets.Script.Input;
using Assets.Script.Pawns.Core;
using Assets.Script.Nodes.Core;
using Assets.Script.Interactions;
using System.Collections;
using DG.Tweening;

namespace Assets.Script.Pawns.Player
{
    public class PlayerController : Pawn
    {
        [SerializeField]
        private InputReader inputReader = default;

        [SerializeField]
        private LayerMask interactMask;

        private Camera cam;
        private Vector2 mousePosition;

        private void OnEnable()
        {
            inputReader.clickEvent += Interact;
            inputReader.mousePositionEvent += OnMousePositon;
            gameManagerProxy.startPlayerTurnEvent += OnStartTurn;
        }

        private void OnDisable()
        {
            inputReader.clickEvent -= Interact;
            inputReader.mousePositionEvent -= OnMousePositon;
            gameManagerProxy.startPlayerTurnEvent -= OnStartTurn;
        }

        protected override void Awake()
        {
            cam = Camera.main;
            base.Awake();
        }

        protected override void OnEnterNode(BaseNode node) => node.Player = this;

        protected override void OnExitNode(BaseNode node) => node.Player = null;

        protected override void OnStartTurn() => currentNode.PlayerStartTurn();

        private void Interact()
        {
            Ray ray = cam.ScreenPointToRay(mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, interactMask)) return;

            var script = hit.collider.GetComponent<IInteract>();
            if (script != null) script.ExecuteInteraction(this);
        }

        private void OnMousePositon(Vector2 pos) => mousePosition = pos;


        public void MoveInteraction(Vector3 target)
        {
            MoveAction(target);
            gameManagerProxy.AddCommandPlaylist(commandPlayList);
        }

        public override IEnumerator Die()
        {
            gameManagerProxy.SetGameStatus(Manager.GameStatus.Lost);

            yield return base.Die();

            gameManagerProxy.startPlayerTurnEvent -= OnStartTurn;

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
