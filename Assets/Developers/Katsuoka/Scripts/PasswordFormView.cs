using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class PasswordFormView : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI UserNameUI;
    [SerializeField] public TMP_InputField UserPasswordUI;

    [SerializeField] public Button OKButtonUI;
    [SerializeField] public Button CancelButtonUI;

    // TestCommonDialog
    [SerializeField] public GameObject commonDialogTest;
    [SerializeField] public GameObject commonDialogPanel;
    [SerializeField] public Button commonDialogOKButton;


    private Action<PasswordDialogResult> _resultCallback;
    private DummyUserClass _user;

    public enum PasswordDialogResult
    {
        Ok,
        Cancel
    }

    public class DummyUserClass
    {
        public string Name;
        public string Password;
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        commonDialogTest.SetActive(false);
        
        // OK Button
        OKButtonUI.onClick.AddListener(() => {

            if (_user.Password != UserPasswordUI.text)
            {
                commonDialogPanel.transform.localScale = Vector3.zero;
                commonDialogPanel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutBack);

                commonDialogTest.SetActive(true);
                return;
            }

            gameObject.SetActive(false);
            _resultCallback?.Invoke(PasswordDialogResult.Ok);
        });

        // Cancael Button
        CancelButtonUI.onClick.AddListener(() => {
            gameObject.SetActive(false);
            _resultCallback?.Invoke(PasswordDialogResult.Cancel);
        });

        // Child Commondialog button
        commonDialogOKButton.onClick.AddListener(() => {
            commonDialogTest.SetActive(false);
        });
    }

    private void OnDestroy()
    {
        OKButtonUI.onClick.RemoveAllListeners();
        OKButtonUI.onClick.RemoveAllListeners();
    }

    public void ShowDialog(DummyUserClass user, Action<PasswordDialogResult> resultCallback)
    {
        _resultCallback = resultCallback;

        UserNameUI.text = user.Name;
        UserPasswordUI.text = "";

        _user = user;
        
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutBack); 

        gameObject.SetActive(true);
    }
}
