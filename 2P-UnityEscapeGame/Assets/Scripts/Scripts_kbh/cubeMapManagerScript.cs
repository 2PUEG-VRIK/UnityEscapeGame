using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// �躸�� �����ڵ�

public class cubeMapManagerScript : MonoBehaviour
{
    //1��- ť������
    public GameObject[] Cubes;
    private bool isHold = false;//���� ����ֳ���~
    Rigidbody rigid;
    theCubes cube;
    GameObject grabCube;//�տ� ����մ� ť��
    int cubeValue;//�տ� ����ִ� ť�� ��
    Ray ray;
    RaycastHit hit;
    Renderer cubeColor;
    int cubeNum = 18;//ť�� ����
    bool goDown;//����ǥ ũ�� ���� ����
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

    public AudioClip audioCubeTouch;//ť��Ծ�����
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
            if (!isHold)//����������� ���¿��� �ָ� ������!
            {
                if (cube.name == "_Cube")
                    Instantiate(hammer_prefab, other.transform.position, Quaternion.identity);
                audioSource.clip = audioCubeTouch;
                audioSource.Play();
                grabCube.transform.gameObject.SetActive(true);//����ְ� �ϰ�
                grabCube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
                cube.gameObject.SetActive(false);//���� �� ���ְ�
                cubeValue = cube.value;//������ ����� �־�
                Debug.Log("��Ѵ� " + cubeValue) ;
                isHold = true;

            }

            else // �� ���¿��� ���ڸ� ��ġ�ص�!
            {
                if (cubeValue == cube.value)//����ִ¾ֶ� ���� �ֶ� ���� ���ٸ�
                {
                    if (cube.name == "_Cube")
                        Instantiate(hammer_prefab, other.transform.position, Quaternion.identity);
                    Debug.Log("���� ��: " + cube.value + "  �ִ� ��:  " + cubeValue);
                    audioSource.clip = audioCubeCorrect;
                    audioSource.Play();
                    cube.transform.gameObject.SetActive(false);//���� �� ���ְ�,, �� �����ϱ�ȴ� ����
                    grabCube.transform.gameObject.SetActive(false);//����ִ¾� ���ְ�
                    cubeValue = -1;
                    isHold = false;
                    cubeNum -= 2;
                }
                else //���� �ٸ���~~~ �ٸ� �ָ� �����!
                {
                    audioSource.clip = audioCubeWrong;
                    audioSource.Play();
                    cube.GetComponent<Renderer>().material.color = Color.red;
                    check = 1;
                    //�� 0.5�� �ڿ� �� ���󺹱�
                    StartCoroutine(restoreColor(cube));

                }
            }

        }

        //if(other.transform.tag=="Water")
        //{
        //    if (!isHold)//����������� ���¿��� �ָ� ������!
        //    {
        //        if (cube.name == "_Cube")
        //            Instantiate(hammer_prefab, other.transform.position, Quaternion.identity);
        //        audioSource.clip = audioCubeTouch;
        //        audioSource.Play();
        //        grabCube.transform.gameObject.SetActive(true);//����ְ� �ϰ�
        //        grabCube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
        //        cube.gameObject.SetActive(false);//���� �� ���ְ�
        //        cubeValue = cube.value;//������ ����� �־�
        //        isHold = true;

        //    }
        //}

        if (other.transform.tag == "Item" && cubeNum==0)
        {
            hammerGrab.SetActive(true);
            goApartment = true;
        }
    }

    IEnumerator goBack()//1������ ���ڶ� ������ �ڷ� ƨ��°�
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
            case 1: // ��ȫ
                cube.GetComponent<Renderer>().material.color = new Color(255f / 255f, 181f / 255f, 242f / 255f, 255f / 255f);
                break;
            case 2: //�����
                cube.GetComponent<Renderer>().material.color = new Color(253f / 255f, 235f / 255f, 103f / 255f, 255f / 255f);
                break;
            case 3://�ϴ�
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