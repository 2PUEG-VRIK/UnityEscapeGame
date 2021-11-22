using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMNG : MonoBehaviour
{
    // 다음 스테이지..
    public string scene;

    bool isFinish;
    bool IsPause;
    Text pause;

    // 시간
    private float timeCount = 0;
    Text state;

    // 결과창 시간
    Text time;

    private void Awake()
    {
        isFinish = false;
        IsPause = false;

        state = GameObject.Find("State").GetComponent<Text>();
        pause = GameObject.Find("Pause").GetComponentInChildren<Text>();
    }

    private void Update()
    {
        Timer();
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


    public void Game_Clear()
    {
        time = GameObject.Find("Time").GetComponent<Text>();
        time.text = string.Format("{0:0.00}", timeCount);
    }


    public void Pause()
    {
        /*일시정지 활성화*/
        if (!IsPause)
        {
            Time.timeScale = 0;
            IsPause = true;
            pause.text = "Resume";
        } 
        else
        {
            Time.timeScale = 1;
            IsPause = false;
            pause.text = "Pause";
        }
    }

    public void Next()
    {
        if (!scene.Equals("end"))
        {
            StartCoroutine(LoadSceneCorutine());
        }
    }

    IEnumerator LoadSceneCorutine()
    {
        yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
            // 어플리케이션 종료
        #endif
    }
}
