using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    BoxCollider boxcollider;
    Rigidbody rigid;
    Vector3 pos;//���� ���� ��ġ
    Vector3 pre_pos;//�̵��ϱ� ���� ���� �� ��ġ
                    // GameObject _obj;//���� ��
    public GameObject _objTrap;//������ Ʈ��
    Man player;//��~
    SpriteRenderer spr;
    private float time = 0f;//¡�˴ٸ��� �ִ� �ð� ���(2��)
    bool isOn = false;//¡�˴ٸ��� �ö���ֵ�
    bool timerOn = false;
    GameObject overHead;//�Ӹ����� ü�� -1 �ߴ°�
    Vector3 overHeadPos;
    private bool isPopUp = false;
    public int value;//������, �������� ���� �ֵ� ���ϴ� �� ; 1�� ������, -1�� �������� �ս���
    public float speed;//���� �����̴� �ӷ�
    private Steps _obj;//���� ���� ��
    //private bool assimilate = false;//���� ����ִ� �����̴� ���̶� �� ��ġ�� ����ȭ

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


        if (this.value == -1)//�������� ������~
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
            if (Input.GetKeyDown(KeyCode.Space)) //collider Exit ����
            {
                time = 0f;
                Debug.Log("�̰� �Ƴ�? �����̽��ٴ����¶�");
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
            Debug.Log("ü�� 0");
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
                _obj = this;//_obj�� ���� ���� ��!

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


    private void Trap()//�Ϲ� �� -> Ʈ�� �Ǿ�
    {
        timerOn = false;
        Instantiate(_objTrap, pos, Quaternion.identity);//���� �ִ� ���� Ʈ�� ����

        _objTrap.transform.localScale = _obj.transform.localScale;
        _objTrap.tag = "Floor";
        _objTrap.gameObject.layer = 7;//Floor layer�� �ٲ���
        _obj.transform.position = new Vector3(999, 999, 999);//��Ҵ� �� ���ְ�~
        _obj.value = 0;
        InvokeRepeating("decreaseHealth", 0.2f, 2f);
    }

    private int count = -1;//ü�±����� ���̴� ����

    private void decreaseHealth()
    {
        count = -1;
        player.health -= 1;
        popUp();
        Debug.Log(player.health);
    }
    private void popUp()//ü�� -1 �Ӹ� ���� ���
    {
        isPopUp = true;
        overHead.SetActive(true);

        Invoke("disappear", 1f);
    }

    private void disappear()
    {
        isPopUp = false;
        overHead.SetActive(false);//�̰� �ٽ� Ǯ�����
    }


}