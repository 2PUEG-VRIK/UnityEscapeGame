using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    BoxCollider boxcollider;
    Rigidbody rigid;
    Vector3 pos;//밟은 땅의 위치
    Vector3 pre_pos;//이동하기 전의 밟은 블럭 위치
                    // GameObject _obj;//밟은 땅
    public GameObject _objTrap;//생성할 트랩
    Man player;//나~
    SpriteRenderer spr;
    private float time = 0f;//징검다리에 있는 시간 재기(2초)
    bool isOn = false;//징검다리에 올라와있따
    bool timerOn = false;
    GameObject overHead;//머리위에 체력 -1 뜨는거
    Vector3 overHeadPos;
    private bool isPopUp = false;
    public int value;//오른쪽, 왼쪽으로 가는 애들 정하는 값 ; 1은 오른쪽, -1은 왼쪽으로 합쉬다
    public float speed;//발판 움직이는 속력
    private Steps _obj;//내가 밟은 땅
    //private bool assimilate = false;//내가 밟고있는 움직이는 땅이랑 내 위치랑 동기화

    Transform playerPos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        player = GameObject.FindWithTag("Player").GetComponent<Man>();
        spr = player.GetComponent<SpriteRenderer>();
        pos = this.transform.position;
        pre_pos = this.transform.position;

    }

    private void Update()
    {
        overHead = GameObject.Find("number").transform.GetChild(0).gameObject;
        pos = this.transform.position;
        playerPos = player.transform;


        if (this.value == -1)//왼쪽으로 가야혀~
        {
            this.transform.position = Vector3.Lerp(pos, new Vector3(pre_pos.x, pre_pos.y, pre_pos.z + 6f), speed);
            if (pos.z > pre_pos.z + 5f)
                value = 1;
        }

        else if (this.value == 1)
        {
            this.transform.position = Vector3.Lerp(pos, new Vector3(pre_pos.x, pre_pos.y, pre_pos.z - 6f), speed);
            if (pos.z < pre_pos.z - 5f)
                value = -1;
        }

        //if (assimilate)
        //    playerPos.transform.position = new Vector3(playerPos.position.x,pos.y, playerPos.position.z );

        if (isOn)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //collider Exit 역할
            {
                time = 0f;
                Debug.Log("이거 아녀? 스페이스바눌렀읐때");
                timerOn = false;
                CancelInvoke("decreaseHealth");
                isOn = false;
                //assimilate = false;
            }
            else if (timerOn)
                Timer();


            else if (isPopUp)
                overHead.transform.position = new Vector3
                    (playerPos.position.x, playerPos.position.y + 12f, playerPos.position.z);

        }

        if (player.health == 0)
            Debug.Log("체력 0");
    }

    private void Timer()
    {
        time += Time.deltaTime;

        if (time >= 3f)
            Trap();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOn = true;

            if (this.gameObject.name == "Step")
            {
                _obj = this;//_obj는 내가 밟은 땅!

                pos = new Vector3(
                    _obj.transform.position.x,
                    _obj.transform.position.y,
                    _obj.transform.position.z);

                timerOn = true;
            }
            else
                InvokeRepeating("decreaseHealth", 0.2f, 2f);
        }
    }


    private void Trap()//일반 블럭 -> 트랩 되어
    {
        timerOn = false;
        Instantiate(_objTrap, pos, Quaternion.identity);//내가 있던 곳에 트랩 생성

        _objTrap.transform.localScale = _obj.transform.localScale;
        _objTrap.tag = "Floor";
        _objTrap.gameObject.layer = 7;//Floor layer로 바꿔줌
        _obj.transform.position = new Vector3(999, 999, 999);//밟았던 곳 없애고~
        _obj.value = 0;
        InvokeRepeating("decreaseHealth", 0.2f, 2f);
    }

    private int count = -1;//체력깎을때 쓰이는 변수

    private void decreaseHealth()
    {
        count = -1;
        player.health -= 1;
        popUp();
        Debug.Log(player.health);
    }
    private void popUp()//체력 -1 머리 위에 띄워
    {
        isPopUp = true;
        overHead.SetActive(true);

        Invoke("disappear", 1f);
    }

    private void disappear()
    {
        isPopUp = false;
        overHead.SetActive(false);//이거 다시 풀어야해
    }


}