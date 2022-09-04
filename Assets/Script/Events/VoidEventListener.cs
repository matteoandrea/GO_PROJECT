using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Events
{
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField] private VoidEventSO _event = default;

        public UnityEvent OnEventRaised;

        private void OnEnable()
        {
            if (_event != null)
                _event.OnEventRaised += Respond;
        }

        private void OnDisable()
        {
            if (_event != null)
                _event.OnEventRaised -= Respond;
        }

        private void Respond()
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke();
        }
    }
}