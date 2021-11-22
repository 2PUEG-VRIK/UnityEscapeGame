using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    // ���� ��������..
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

    // �Ʒ� �Լ� 2�� �ٲ�.
    private void OnTriggerEnter(Collider other)
    {
        if (!isFinish && other.gameObject.tag == "Player")
        {

            players = other.gameObject;

            // Ű���� �̵� �ȹ޴� �ڵ� �߰�

            SingleGameMNG.Instance.Timer_Stop();
            StartCoroutine("StageFinish");
        }
    }
    IEnumerator StageFinish()
    {

        // �ϴ÷� �ö󰡶�.
        yield return new WaitForSeconds(1f);
        players.GetComponent<Rigidbody>().useGravity = false;
        isFinish = true;

        // ���â ����
        yield return new WaitForSeconds(2f);
        result.SetActive(true);

        // ��� â�� �ð� ���� �Լ�
        SingleGameMNG.Instance.Game_Clear();
    }
}