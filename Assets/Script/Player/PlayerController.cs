using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Assets.Script.Input;
using Assets.Script.Events;

namespace Assets.Script.Player
{
    public class PlayerController : MonoBehaviour,ICommnad
    {
        [SerializeField] private InputReader _inputReader = default;
        [SerializeField] private VoidEventSO playerMovedEvent;

        private Camera _camera;
        private NavMeshAgent _agent;

        private Vector2 _mousePosition;

        private void Awake()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            _inputReader.clickEvent += MovePlayer;
            _inputReader.mousePositionEvent += OnMousePositon;
        }

        private void OnDisable()
        {
            _inputReader.clickEvent -= MovePlayer;
            _inputReader.mousePositionEvent -= OnMousePositon;
        }

        public void Execute()
        {
            MovePlayer();
        }

        private void OnMousePositon(Vector2 pos)
        {
            _mousePosition = pos;
        }

        private void MovePlayer()
        {
            Ray ray = _camera.ScreenPointToRay(_mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit)) return;

            var path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, hit.point, NavMesh.AllAreas, path);
            _agent.SetPath(path);
            var link = _agent.nextOffMeshLinkData.endPos;
            NavMesh.CalculatePath(transform.position, link, NavMesh.AllAreas, path);
            _agent.SetPath(path);

            playerMovedEvent.OnEventRaised.Invoke();
        }
    }
}