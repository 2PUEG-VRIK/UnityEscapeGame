using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class monsterMapScript : MonoBehaviour
{
  
    //2층
    GameObject _obj;
    GameObject holdPosition;
    GameObject scrLight;
    public GameObject input;//light 입력받는 
    public Text text;
    SpriteRenderer sr;//sprite renderer 
    int check = -1;
    Image img;
    public int monNum;
    GameObject nearObject;
    Man coinCheck;
    GameObject Door;
    private int open = 0;//문 열어
    bool isBack = false;//뒤로 한번 튕겨야지
    private GameObject target;//마우스가 클릭한 객체
    private bool _mouseState;//마우스 상태
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
        //2층
        if (Input.GetKeyDown(KeyCode.Return))//엔터누르면 
        {
            //문자열이랑 light랑 비교
            if (string.Compare("home", text.text, true) == 0)//정답
            {
                Answer();
            }
            else //lightㄱㅏ 아니면~
            {
                Wrong();
                Invoke("tryAgain", 0.5f);
            }
        }

        if (coinCheck.check == 1)//동전 들고있ㄷ고
        {
            holdPosition.transform.localPosition = new Vector3(0, 0.27f, -0.25f);
            _obj = GameObject.Find("holdingCoin").transform.GetChild(0).gameObject;
            _obj.SetActive(true);//동전 눈에 보이게

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

        if (goApartment)//아파트있는 맵으로 돌아가기
            StartCoroutine(goApartmentCo());
    }

    private void mumchwo()//update에서 isBack=false하면 뒤로 가기도 전에 멈춰버려서~ 안됨
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

        scrLight.transform.rotation = Quaternion.Euler(90, 0, 0);//암전
        _obj.SetActive(false);//What we need 없애
        _obj = null;

        _obj = GameObject.Find("Weapons").transform.GetChild(0).gameObject;
        _obj.SetActive(true);

        for (int j = 0; j < monNum; j++)
        {
            _obj = GameObject.Find("Monsters").transform.GetChild(j).gameObject;
            _obj.SetActive(true);
        }

        //주석주석주석이~
    }
    private void tryAgain()
    {
        img.color = new Color(168, 206, 255, 192);
        text.text.Replace(text.text, " ");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Exit")/////////////////exit에 닿으면~
        {
            judge.yes = true;
            goApartment = true;
        }
        if (other.tag == "Things")
        {
            if (other.name == "teleB")
            {
                Destroy(other.gameObject);

                scrLight.transform.rotation = Quaternion.Euler(-90, 0, 0);//빛 off
                _obj = GameObject.Find("Canvas_2").transform.GetChild(0).gameObject;//text임
                _obj.SetActive(true);//what we eneed켜
                input.SetActive(true);//입력받는 창 켜

                check = 2;
            }
            if (other.name == "Door")//동전 들고 문 앞에 가면coinCheck.check==1
            {
                //문열어
                isBack = true;//뒤로 튕길 준비 완.
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



