using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Script.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Managers/Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions
    {
        //Gameplay
        public event UnityAction clickEvent;
        public event UnityAction<Vector2> mousePositionEvent;

        private GameInput _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Gameplay.SetCallbacks(this);
            }

            EnableGameplayInput();
        }

        private void OnDisable()
        {
            _gameInput.Disable();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (clickEvent == null) return;

            switch (context.phase)
            {
                case InputActionPhase.Disabled:
                    break;
                case InputActionPhase.Waiting:
                    break;
                case InputActionPhase.Started:
                    break;
                case InputActionPhase.Performed:
                    clickEvent.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    break;
                default:
                    break;
            }
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            if (mousePositionEvent == null) return;

            mousePositionEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void DisableAll()
        {
            _gameInput.Gameplay.Disable();
        }

        public void EnableGameplayInput()
        {
            DisableAll();
            _gameInput.Gameplay.Enable();
        }
    }
}