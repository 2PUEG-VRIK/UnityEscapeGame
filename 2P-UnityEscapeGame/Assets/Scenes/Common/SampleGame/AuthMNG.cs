using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions;

public class AuthMNG : MonoBehaviour
{
    public Text logText;
    public UnityEngine.UI.Button signInBtn, signUpBtn;
    public InputField email, password;



    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            Firebase.DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
            }
            else
            {
                Debug.LogError("Counld not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });  
        
    }

    public void OnClickSignUp()
    {
        // ȸ�� ����
        Debug.Log("OnClickSignUp");
        Debug.Log("ȸ�� ���� �� �̸��� : " + email.text + " ��� ��ȣ : " + password.text);

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {   
                Debug.LogError("Come Here Fail");
                logText.text = "SignIn With EmailAndPasswordAsync was canceled";
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Come Here Fault");
                logText.text = task.Exception.ToString();
                return;
            }
            else
            {
                Debug.Log("Come Here Finish");
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            }
        });
    }

    public void OnClickSignIn()
    {
        // �α���
        Debug.Log("OnClickSignIn");
        Debug.Log("�α��� �� �̸��� : " + email.text + " ��� ��ȣ : " + password.text);

        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("SignIn Fail");
                Debug.LogError("SignIn With EmailAndPasswordAsync was canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("SignIn Fault");
                Debug.LogError("SignIn With EmailAndPasswordAsync was Faulted : " + task.Exception);
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
        });

    }

}
