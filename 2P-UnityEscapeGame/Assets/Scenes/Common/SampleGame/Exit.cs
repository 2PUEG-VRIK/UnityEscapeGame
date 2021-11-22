using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    // 다음 스테이지..
    public string scene;
    public GameObject result;

    GameObject players;
    bool isFinish;

    private void Start()
    {
        isFinish = false;
    }

    private void Update()
    {
        FinishMove();
    }

    public void FinishMove()
    {
        if (isFinish)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * 15);
            players.transform.position = Vector3.MoveTowards(players.transform.position, players.transform.position + Vector3.up, Time.deltaTime * 15);
        }
    }

    // 아래 함수 2개 바뀜.
    private void OnTriggerEnter(Collider other)
    {
        if (!isFinish && other.gameObject.tag == "Player")
        {

            players = other.gameObject;

            // 키보드 이동 안받는 코드 추가

            SingleGameMNG.Instance.Timer_Stop();
            StartCoroutine("StageFinish");
        }
    }
    IEnumerator StageFinish()
    {

        // 하늘로 올라가라.
        yield return new WaitForSeconds(1f);
        players.GetComponent<Rigidbody>().useGravity = false;
        isFinish = true;

        // 결과창 띄우기
        yield return new WaitForSeconds(2f);
        result.SetActive(true);

        // 결과 창에 시간 띄우는 함수
        SingleGameMNG.Instance.Game_Clear();
    }
}