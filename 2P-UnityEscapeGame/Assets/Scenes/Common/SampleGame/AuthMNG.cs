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
        // 회원 가입
        Debug.Log("OnClickSignUp");
        Debug.Log("회원 가입 할 이메일 : " + email.text + " 비밀 번호 : " + password.text);

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
        // 로그인
        Debug.Log("OnClickSignIn");
        Debug.Log("로그인 할 이메일 : " + email.text + " 비밀 번호 : " + password.text);

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
