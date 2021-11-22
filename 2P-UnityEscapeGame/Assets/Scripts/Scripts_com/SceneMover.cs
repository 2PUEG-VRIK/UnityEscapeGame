using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 이렇게 얘만 작동하는 코드를 쓰거나
            //SceneManager.LoadScene("Scene2", LoadSceneMode.Single);

            // 이건 다른 작업도 할수 있게끔 코루틴 사용한거
            StartCoroutine(LoadSceneCorutine());
        }
    }

    IEnumerator LoadSceneCorutine()
    {
        yield return SceneManager.LoadSceneAsync("Scene2", LoadSceneMode.Additive);
    }
}
