using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class SingleGameMNG : MonoBehaviour
{
    private static SingleGameMNG _instance;
    //public string playername;
    private float sum;

    bool isFinish;
    bool IsPause;
    Text pause;

    //private int myrank = -1;

    // 시간
    private float timeCount = 0.0f;
    Text state;
    Text mode;

    // 결과창 시간
    Text time;
    //private readonly string DBurl = "https://realtime-unity-default-rtdb.firebaseio.com/";
    private string now_scene = "Start Scene";
    private string activeScene;




    // 인스턴스에 접근하기 위한 프로퍼티
    public static SingleGameMNG Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SingleGameMNG)) as SingleGameMNG;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        isFinish = false;
        IsPause = false;
    }

    private void Update()
    {
        activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "A_Start Scene" || now_scene == "Finish Scene")
        {
        }
        else if (activeScene == now_scene)
        {
            mode = GameObject.Find("Mode").GetComponent<Text>();
            mode.text = "\nStage " + activeScene.Substring(activeScene.Length - 2, 2);

            state = GameObject.Find("State").GetComponent<Text>();
            Timer(); // 타이머 가동은 계속 해야지
        }
        else
        {
            Debug.Log("Scene Change!!");
            now_scene = activeScene;

            if (activeScene == "Finish Scene")
            {
                Debug.Log("Now is Finish Scene");
            }
            else
            {
                isFinish = false;
                timeCount = 0; // 시간 0으로 돌리고

                mode = GameObject.Find("Mode").GetComponent<Text>();
                mode.text = "\nStage " + activeScene.Substring(activeScene.Length - 1, 1);

                state = GameObject.Find("State").GetComponent<Text>();

                Timer();
            }
        }
    }


    public void Game_Clear()
    {
        // 결과 창에 시간 띄우는 코드
        time = GameObject.Find("Time").GetComponent<Text>();
        time.text = string.Format("{0:0.00}", timeCount);
    }

    public string getSum()
    {
        return string.Format("{0:0.00}", sum);
    }

    public void Next()
    {
        string scene = GameObject.Find("Exit").GetComponent<Exit>().scene;
        Debug.Log(scene);
        StartCoroutine(LoadSceneCorutine(scene));
    }

    IEnumerator LoadSceneCorutine(string scene)
    {
        yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    IEnumerator LoadSceneCorutineByIndex(int index)
    {
        yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
    }

    public void Retry()
    {
        StartCoroutine(LoadSceneCorutineByIndex(SceneManager.GetActiveScene().buildIndex));
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit() ;
            // 어플리케이션 종료
#endif
    }

    public void Pause()
    {
        Debug.Log("Pause");
        /*일시정지 활성화*/
        if (!IsPause)
        {
            Time.timeScale = 0;
            IsPause = true;
        }
        else
        {
            Time.timeScale = 1;
            IsPause = false;
        }
    }

    public void Timer()
    {
        if (isFinish)
        {
            return;
        }
        else
        {
            timeCount += Time.deltaTime;
            state.text = "Time : " + string.Format("{0:0.00}", timeCount);
        }
    }

    public void Timer_Stop()
    {
        isFinish = true;
    }

}
