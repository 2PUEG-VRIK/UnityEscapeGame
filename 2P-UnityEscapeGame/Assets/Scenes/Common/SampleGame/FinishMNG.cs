using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class FinishMNG : MonoBehaviour
{

    private void Start()
    {


    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit() ;
            // 어플리케이션 종료
#endif
    }

    public void RestartGame()
    {

        SceneManager.LoadScene("Stage01");

    }

}

