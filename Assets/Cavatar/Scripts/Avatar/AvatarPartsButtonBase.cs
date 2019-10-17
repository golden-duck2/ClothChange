using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AvatarPartsButtonBase : MonoBehaviour
{
    [SerializeField] Image fontNew = default;
    [SerializeField] Button partsButton = default;

    public void AddOnClickEventListener(UnityAction unityAction)
    {
        partsButton.onClick.AddListener(unityAction);
    }
}