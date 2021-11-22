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

    // �ð�
    private float timeCount = 0.0f;
    Text state;
    Text mode;

    // ���â �ð�
    Text time;
    //private readonly string DBurl = "https://realtime-unity-default-rtdb.firebaseio.com/";
    private string now_scene = "Start Scene";
    private string activeScene;




    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    public static SingleGameMNG Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
            Timer(); // Ÿ�̸� ������ ��� �ؾ���
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
                timeCount = 0; // �ð� 0���� ������

                mode = GameObject.Find("Mode").GetComponent<Text>();
                mode.text = "\nStage " + activeScene.Substring(activeScene.Length - 1, 1);

                state = GameObject.Find("State").GetComponent<Text>();

                Timer();
            }
        }
    }


    public void Game_Clear()
    {
        // ��� â�� �ð� ���� �ڵ�
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
            // ���ø����̼� ����
#endif
    }

    public void Pause()
    {
        Debug.Log("Pause");
        /*�Ͻ����� Ȱ��ȭ*/
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
