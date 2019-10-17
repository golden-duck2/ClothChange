using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


namespace Jam
{
    public class Operation : MonoBehaviour
    {
/*
        [Serializable]
        public class ButtonClickedEvent2 : UnityEvent { }        // GameObject class methodName param0 param1

        // Event delegates triggered on click.
        [FormerlySerializedAs("onClick")]
        [SerializeField]
        private ButtonClickedEvent2 m_OnClick = new ButtonClickedEvent2();

        // Event delegates triggered on click.
        [SerializeField]
        private ButtonClickedEvent2 m_OnClick2 = new ButtonClickedEvent2();
*/


        [System.Serializable]
        public struct OperationData
        {
            [SerializeField]
            public GameObject gameObject;

            [SerializeField]
            public string monoBehaviourName;

            [SerializeField]
            public string methodName;
            [SerializeField]
            public string parameters;
        }

        [SerializeField]
        public OperationData[] operationDataArray;

        /*        void Start()
                {
                    Debug.Log("at Start()");
                    foreach (var data in operationDataArray)
                    {
                        if (data.gameObject == null)
                        {
                            continue;
                        }
                        Debug.Log("go:" + data.gameObject.name);
                        var monoBehaviour = data.gameObject.GetComponent(data.monoBehaviourName);
                        var methodInfo = monoBehaviour.GetType().GetMethod(data.methodName);
                        if (methodInfo != null)
                        {
                            //                    methodInfo.Invoke(targetProp, new object[] { modifiedValue });
                            //                    methodInfo.Invoke(monoBehaviour, new object[] { null });
                            methodInfo.Invoke(monoBehaviour, null);
                        }
                    }
                }
        */

        public void Operate()
        {
            StartCoroutine(OperationCoroutine());
        }
        IEnumerator OperationCoroutine( /*float waitTime, Action action*/ )
        {
            foreach (var data in operationDataArray)
            {
                if (data.gameObject)
                { // 通常処理.
                    var monoBehaviour = data.gameObject.GetComponent(data.monoBehaviourName);
                    if (monoBehaviour == null)
                    {
                        //                        Debug.LogWarning("Warning. monoBehaviour is null.");
                        // GameObjectに対する処理.
                        var methodInfo = data.gameObject.GetType().GetMethod(data.methodName);
                        if (methodInfo == null)
                        {
                            Debug.LogWarning("Warning. methodInfo is null.");
                            break;
                        }
                        data.gameObject.SetActive(true);
                        //                        methodInfo.Invoke(monoBehaviour, null);
                        break;
                    }
                    if (data.methodName != "")
                    {
                        var methodInfo = monoBehaviour.GetType().GetMethod(data.methodName);
                        if (methodInfo == null)
                        {
                            Debug.LogWarning("Warning. methodInfo is null.");
                            break;
                        }
                        methodInfo.Invoke(monoBehaviour, null);
                    }
                }
                else
                { // 特殊処理.
                    switch (data.methodName)
                    {
                        case "Debug.Log":
                            Debug.Log(data.parameters);
                            break;
                        case "WaitForSeconds":
                            yield return new WaitForSeconds(data.parameters.ParseInt(0));
                            break;
                    }
                }
            }
            Debug.Log("at OperationCoroutine() end");
        }

        public void Fire()
        {
            Debug.Log("at Fire()");
        }
    }
}