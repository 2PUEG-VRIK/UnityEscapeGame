using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class monsterMapScript : MonoBehaviour
{
  
    //2��
    GameObject _obj;
    GameObject holdPosition;
    GameObject scrLight;
    public GameObject input;//light �Է¹޴� 
    public Text text;
    SpriteRenderer sr;//sprite renderer 
    int check = -1;
    Image img;
    public int monNum;
    GameObject nearObject;
    Man coinCheck;
    GameObject Door;
    private int open = 0;//�� ����
    bool isBack = false;//�ڷ� �ѹ� ƨ�ܾ���
    private GameObject target;//���콺�� Ŭ���� ��ü
    private bool _mouseState;//���콺 ����
    GameObject exit;
    private bool goApartment;
    judginScript judge;


    private void Start()
    {
        scrLight = GameObject.Find("Directional Light");
        sr = input.GetComponent<SpriteRenderer>();
        img = input.GetComponent<Image>();
        coinCheck = GameObject.Find("Man").GetComponent<Man>();
        img = input.GetComponent<Image>();
        Door = GameObject.Find("Door_5.001");
        exit = GameObject.Find("2nd").transform.GetChild(5).gameObject;
        goApartment = false;
        holdPosition = GameObject.Find("holdingCoin");
        judge = GameObject.Find("judging").GetComponent<judginScript>();

    }
    private void Awake()
    {
    }
    private void Update()
    {
        //2��
        if (Input.GetKeyDown(KeyCode.Return))//���ʹ����� 
        {
            //���ڿ��̶� light�� ��
            if (string.Compare("home", text.text, true) == 0)//����
            {
                Answer();
            }
            else //light���� �ƴϸ�~
            {
                Wrong();
                Invoke("tryAgain", 0.5f);
            }
        }

        if (coinCheck.check == 1)//���� ����֤���
        {
            holdPosition.transform.localPosition = new Vector3(0, 0.27f, -0.25f);
            _obj = GameObject.Find("holdingCoin").transform.GetChild(0).gameObject;
            _obj.SetActive(true);//���� ���� ���̰�

            if (open == 1)
            {
                if (isBack)
                {
                    this.transform.position = Vector3.Lerp(
                        this.transform.position, new Vector3(724, 96, 444), Time.deltaTime * 2);

                    Invoke("mumchwo", 1);
                }
                _obj.SetActive(false);
                exit.SetActive(true);
                Door.transform.rotation = Quaternion.Slerp(
                Door.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.time * 0.001f);
                Door.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }
        }

        if (goApartment)//����Ʈ�ִ� ������ ���ư���
            StartCoroutine(goApartmentCo());
    }

    private void mumchwo()//update���� isBack=false�ϸ� �ڷ� ���⵵ ���� ���������~ �ȵ�
    {
        isBack = false;
        _obj = GameObject.Find("2nd").transform.GetChild(4).gameObject;
        _obj.SetActive(true);
    }

    private void Wrong()
    {
        img.color = Color.red;
        text.text = "";
    }
    private void Answer()
    {
        Destroy(input.gameObject);

        scrLight.transform.rotation = Quaternion.Euler(90, 0, 0);//����
        _obj.SetActive(false);//What we need ����
        _obj = null;

        _obj = GameObject.Find("Weapons").transform.GetChild(0).gameObject;
        _obj.SetActive(true);

        for (int j = 0; j < monNum; j++)
        {
            _obj = GameObject.Find("Monsters").transform.GetChild(j).gameObject;
            _obj.SetActive(true);
        }

        //�ּ��ּ��ּ���~
    }
    private void tryAgain()
    {
        img.color = new Color(168, 206, 255, 192);
        text.text.Replace(text.text, " ");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Exit")/////////////////exit�� ������~
        {
            judge.yes = true;
            goApartment = true;
        }
        if (other.tag == "Things")
        {
            if (other.name == "teleB")
            {
                Destroy(other.gameObject);

                scrLight.transform.rotation = Quaternion.Euler(-90, 0, 0);//�� off
                _obj = GameObject.Find("Canvas_2").transform.GetChild(0).gameObject;//text��
                _obj.SetActive(true);//what we eneed��
                input.SetActive(true);//�Է¹޴� â ��

                check = 2;
            }
            if (other.name == "Door")//���� ��� �� �տ� ����coinCheck.check==1
            {
                //������
                isBack = true;//�ڷ� ƨ�� �غ� ��.
                open = 1;
            }
        }
    }

    IEnumerator goApartmentCo()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("Stage10");
        while (!async.isDone)
            yield return null;
    }

}



