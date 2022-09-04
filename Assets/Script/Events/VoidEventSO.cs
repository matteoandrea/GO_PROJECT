using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.Events
{
    [CreateAssetMenu(menuName = "Events/Void Event")]
    public class VoidEventSO : ScriptableObject
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            if (OnEventRaised != null) OnEventRaised.Invoke();
        }
    }
}