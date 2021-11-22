using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_solo : MonoBehaviour
{

    public GameObject result;
    public GameObject GameManager;
    GameMNG GameMNG;

    GameObject player;
    bool isFinish;

    // Start is called before the first frame update
    void Start()
    {
        GameMNG = GameManager.GetComponent<GameMNG>();
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        FinishMove();
    }

    private void FinishMove()
    {
        if (isFinish)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * 15);
            player.transform.position = Vector3.MoveTowards(player.transform.position, player.transform.position + Vector3.up, Time.deltaTime * 15);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFinish && other.gameObject.tag == "Player")
        {
            Debug.Log("이게 왜 실해잉 안된다");

            GameMNG.Timer_Stop();
            StartCoroutine("StageFinish");

        }

        
    }
    IEnumerator StageFinish()
    {
        yield return new WaitForSeconds(1f);
        player.GetComponent<Rigidbody>().useGravity = false;


        isFinish = true;

        // 결과창 띄우기
        yield return new WaitForSeconds(2f);
        result.SetActive(true);
        GameMNG.Game_Clear();
    }
}
