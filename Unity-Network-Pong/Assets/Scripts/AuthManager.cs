﻿using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    // 현재 App을 실행하는 환경이 파이어베이스를 구동할 수 있는 상황인지를 리턴해주는 bool 프로퍼티
    public bool IsFirebaseReady { get; private set; }
    // 현재 이미 로그인 과정을 진행하고 있는지 리턴해주는 bool 프로퍼티
    public bool IsSignInOnProgress { get; private set; }

    // 해당 값들을 가져오기 위한 변수
    public InputField emailField;
    public InputField passwordField;
    public Button signInButton;

    // 파이어베이스 전체 App을 관리하는 오브젝트 , Authentication을 집중적으로 관리하는 오브젝트
    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;

    // FirebaseAuth를 통해서 이메일,패스워드를 가진 사용자의 정보를 가져와서 할당시키는 변수
    public static FirebaseUser User;

    public void Start()
    {
        signInButton.interactable = false; // 일단 버튼 비활성화

        // 구동 가능한지 체크 & Dependencise fix
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var result = task.Result;

                if(result != DependencyStatus.Available)
                {
                    Debug.LogError(result.ToString());
                    IsFirebaseReady = false;
                }
                else
                {
                    IsFirebaseReady = true;

                    firebaseApp = FirebaseApp.DefaultInstance;
                    firebaseAuth = FirebaseAuth.DefaultInstance;
                }

                signInButton.interactable = IsFirebaseReady;
            }
        );
    }

    public void SignIn()
    {
        // 준비x || 직전의 로그인 신호가 진행 중 || 이미 유저가 할당된 경우
        if(!IsFirebaseReady || IsSignInOnProgress || User != null)
        {
            return;
        }

        IsSignInOnProgress = true;
        signInButton.interactable = false;

        // 종료될 시 실행될 체인 걸기
        firebaseAuth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWithOnMainThread(task => {
                Debug.Log($"Sign in status : {task.Status}");

                IsSignInOnProgress = false;
                signInButton.interactable = true;

                // 예외 처리 
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
                else if (task.IsCanceled)
                {
                    Debug.LogError("Sign-in canceled");
                }
                else // 결과 확인
                {
                    User = task.Result;
                    Debug.Log(User.Email);
                    SceneManager.LoadScene("Lobby");
                }
            }
        );
    }
}