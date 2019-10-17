using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Jam
{
    public class EventInvoker : MonoBehaviour
    {
        public enum Kind
        {
            Awake,
            Start,
            OnEnable,
            OnDisable
        }
        [SerializeField]
        Kind kind = Kind.Start;
        [SerializeField]
        float delaySec = 0.0f;
        [SerializeField]
        UnityEvent invokeEvent = new UnityEngine.Events.UnityEvent();

        void Awake()
        {
            if (kind == Kind.Awake)
            {
                Ignite();
            }
        }

        void Start()
        {
            if (kind == Kind.Start)
            {
                Ignite();
            }
        }

        void OnEnable()
        {
            if (kind == Kind.OnEnable)
            {
                Ignite();
            }
        }

        void OnDisable()
        {
            if (kind == Kind.OnDisable)
            {
                Ignite();
            }
        }

        void Ignite()
        {
            if (delaySec <= 0.0f)
            {
                invokeEvent.Invoke();
            }
            else
            {
                StartCoroutine("WaitDelay");
            }
        }

        IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(delaySec);
            invokeEvent.Invoke();
        }
    }
}
