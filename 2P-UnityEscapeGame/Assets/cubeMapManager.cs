using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cubeMapManager : MonoBehaviour
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
    int cubeNum = 10;//큐브 개수
    bool goDown;//느낌표 크기 관련 변수
    GameObject tele;

    int check = -1;
    GameObject exit;

    private void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        //input = GameObject.Find("Canvas_2").transform.GetChild(1).gameObject;
        rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
        grabCube = GameObject.Find("holdingCube").transform.GetChild(0).gameObject;
        //tele = GameObject.Find("final").transform.GetChild(0).gameObject;
        //exit = GameObject.Find("2nd").transform.GetChild(5).gameObject;
    }

    private void Update()
    {
        if (isHold && check == 1)
            StartCoroutine("goBack");

        if (cubeNum == 0)
        {
            Debug.Log("큐부 0개남음!");


        }

        if (cubeNum == 2)
            Debug.Log("두개남앗써용");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Things")
        {
            if (other.transform.name == "Color_Sphere")
            {
                cube = other.transform.gameObject.GetComponent<theCubes>();
                if (!isHold)//들고있지않은 상태에서 애를 만났따!
                {
                    grabCube.transform.gameObject.SetActive(true);//들고있게 하고
                    grabCube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
                    cube.gameObject.SetActive(false);//닿은 애 없애고
                    cubeValue = cube.value;//변수에 밸류값 넣어
                    isHold = true;
                }

                else // 든 상태에서 상자를 터치해따!
                {
                    if (cubeValue == cube.value)//들고있는애랑 닿은 애랑 값이 같다면
                    {
                        cube.transform.gameObject.SetActive(false);//닿은 애 없애고,, ㅜ 존나하기싫다 쉽발
                        grabCube.transform.gameObject.SetActive(false);//들고있는애 없애고
                        cubeValue = -1;
                        isHold = false;
                        cubeNum -= 2;
                    }
                    else //값이 다르다~~~ 다른 애를 찍엇다!
                    {
                        cube.GetComponent<Renderer>().material.color = Color.red;
                        check = 1;
                        //한 0.5초 뒤에 색 원상복구
                        StartCoroutine(restoreColor(cube));

                    }
                }
            }
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

}