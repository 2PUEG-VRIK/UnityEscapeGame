using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class result : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next()
    {
        SingleGameMNG.Instance.Next();
    }
    public void Retry()
    {
        SingleGameMNG.Instance.Retry();
    }

    public void Quit()
    {
        SingleGameMNG.Instance.Quit();
    }

    public void Pause()
    {
        SingleGameMNG.Instance.Pause();
    }

    //public void NowScene()
    //{
    //    Singletom.Instance.NowScene();
    //}

}
