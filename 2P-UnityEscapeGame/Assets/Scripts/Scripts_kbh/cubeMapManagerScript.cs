using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 김보현 개인코드

public class cubeMapManagerScript : MonoBehaviour
{
    //1층- 큐브들관련
    public GameObject[] Cubes;
    private bool isHold = false;//상자 들고있나여~
    Rigidbody rigid;
    theCubes cube;
    GameObject grabCube;//손에 들고잇는 큐브
    int cubeValue;//손에 들고있는 큐브 값
    Ray ray;
    RaycastHit hit;
    Renderer cubeColor;
    int cubeNum = 18;//큐브 개수
    bool goDown;//느낌표 크기 관련 변수
    GameObject tele;
    private bool goApartment = false;
    int check = -1;
    GameObject exit;
    public GameObject hammer_prefab;
    GameObject hammerGrab;
    judginScript judge;
    GameObject howTo;
    float time;
    private bool isTimerOn;

    public AudioClip audioCubeTouch;//큐브먹었을때
    public AudioClip audioCubeCorrect;
    public AudioClip audioCubeWrong;
    AudioSource audioSource;


    private void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
        grabCube = GameObject.Find("holdingCube").transform.GetChild(0).gameObject;
        //exit = GameObject.Find("2nd").transform.GetChild(5).gameObject;
        hammerGrab = GameObject.Find("WeaponPoint").transform.GetChild(0).gameObject;
        judge = GameObject.Find("judging").GetComponent<judginScript>();
        howTo = GameObject.Find("bbo").transform.GetChild(0).gameObject;
        time = 0.0f;
        cubeNum = 18;
        isTimerOn = true;

    }
    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (isHold && check == 1)
            StartCoroutine("goBack");

        if (cubeNum == 0)
        {
            GameObject.Find("Weapon Hammer").GetComponent<SphereCollider>().isTrigger = true;
            judge.yes_2 = true;
            judge.yes = true;

            if (goApartment)
                StartCoroutine(goApartmentCo());

        }
        if (isTimerOn)
        {
            time += Time.deltaTime;
            StartCoroutine(popHowTo());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Things")
        {
            cube = other.transform.gameObject.GetComponent<theCubes>();
            if (!isHold)//들고있지않은 상태에서 애를 만났따!
            {
                if (cube.name == "_Cube")
                    Instantiate(hammer_prefab, other.transform.position, Quaternion.identity);
                audioSource.clip = audioCubeTouch;
                audioSource.Play();
                grabCube.transform.gameObject.SetActive(true);//들고있게 하고
                grabCube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
                cube.gameObject.SetActive(false);//닿은 애 없애고
                cubeValue = cube.value;//변수에 밸류값 넣어
                Debug.Log("닿앗다 " + cubeValue) ;
                isHold = true;

            }

            else // 든 상태에서 상자를 터치해따!
            {
                if (cubeValue == cube.value)//들고있는애랑 닿은 애랑 값이 같다면
                {
                    if (cube.name == "_Cube")
                        Instantiate(hammer_prefab, other.transform.position, Quaternion.identity);
                    Debug.Log("닿은 애: " + cube.value + "  있던 애:  " + cubeValue);
                    audioSource.clip = audioCubeCorrect;
                    audioSource.Play();
                    cube.transform.gameObject.SetActive(false);//닿은 애 없애고,, ㅜ 존나하기싫다 쉽발
                    grabCube.transform.gameObject.SetActive(false);//들고있는애 없애고
                    cubeValue = -1;
                    isHold = false;
                    cubeNum -= 2;
                }
                else //값이 다르다~~~ 다른 애를 찍엇다!
                {
                    audioSource.clip = audioCubeWrong;
                    audioSource.Play();
                    cube.GetComponent<Renderer>().material.color = Color.red;
                    check = 1;
                    //한 0.5초 뒤에 색 원상복구
                    StartCoroutine(restoreColor(cube));

                }
            }

        }

        //if(other.transform.tag=="Water")
        //{
        //    if (!isHold)//들고있지않은 상태에서 애를 만났따!
        //    {
        //        if (cube.name == "_Cube")
        //            Instantiate(hammer_prefab, other.transform.position, Quaternion.identity);
        //        audioSource.clip = audioCubeTouch;
        //        audioSource.Play();
        //        grabCube.transform.gameObject.SetActive(true);//들고있게 하고
        //        grabCube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
        //        cube.gameObject.SetActive(false);//닿은 애 없애고
        //        cubeValue = cube.value;//변수에 밸류값 넣어
        //        isHold = true;

        //    }
        //}

        if (other.transform.tag == "Item" && cubeNum==0)
        {
            hammerGrab.SetActive(true);
            goApartment = true;
        }
    }

    IEnumerator goBack()//1층에서 상자랑 닿으면 뒤로 튕기는거
    {
        ///rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
        this.transform.Translate(new Vector3(0, 0, -40) * Time.deltaTime);

        check = -1;
        isHold = true;

        yield return null;

    }
    IEnumerator restoreColor(theCubes cube)
    {

        yield return new WaitForSeconds(0.5f);

        switch (cube.value)
        {
            case 1: // 분홍
                cube.GetComponent<Renderer>().material.color = new Color(255f / 255f, 181f / 255f, 242f / 255f, 255f / 255f);
                break;
            case 2: //노란색
                cube.GetComponent<Renderer>().material.color = new Color(253f / 255f, 235f / 255f, 103f / 255f, 255f / 255f);
                break;
            case 3://하늘
                cube.GetComponent<Renderer>().material.color = new Color(110f / 255f, 241f / 255f, 255f / 255f, 255f / 255f);
                break;
        }
    }

    IEnumerator goApartmentCo()
    {

        AsyncOperation async = SceneManager.LoadSceneAsync("Stage10");
        while (!async.isDone)
            yield return null;
    }

    IEnumerator popHowTo()
    {
        if (10f < time && time < 15f)
            howTo.SetActive(true);
        else if (time > 14f)
        {
            howTo.SetActive(false);
            isTimerOn = false;
        }
        yield return null;
    }
}