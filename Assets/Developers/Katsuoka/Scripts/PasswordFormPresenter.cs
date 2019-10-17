using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;
using static PasswordFormView;

public class PasswordFormPresenter : MonoBehaviour
{
    [SerializeField] PasswordFormView view;

    private async void Start()
    {
        await new WaitForSeconds(0.1f);

        var testUser = new DummyUserClass();

        testUser.Name = "Test User";
        testUser.Password = "Password";
        
        view.ShowDialog(testUser, result => {
            switch (result)
            {
                case PasswordDialogResult.Ok:
                    Debug.Log("Password result OK");
                    break;
                case PasswordDialogResult.Cancel:
                    Debug.Log("Password result Cancel");
                    break;
            }
        });

    }
    
}
