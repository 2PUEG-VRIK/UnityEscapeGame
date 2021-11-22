using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Permanent : MonoBehaviour
{
    //몬스터와 징검다리 관할
    GameObject[] m;//초록 몬스터 배열
    GameObject[] Walls;//회전할 벽들
    private int wallNum;//grave wall 개수
    GameObject Step; //징검다리 + 그 위의 아이템

    public int Total;
    private static int  Gnum;
    public AudioClip audioFenceOpen;
    public AudioClip audioInform;
    AudioSource audioSource;
    private int check = 0;
    private int check_p = 0;//
    private int check_t = 0;//몬스터는 벽을~
    float time;
    private bool isTimerOn;
    GameObject howTo;
    
    private void Start()
    {
        time = 0.0f;
        howTo = GameObject.Find("bbo").transform.GetChild(0).gameObject;
        isTimerOn = true;
    }
    private void Awake()
    {
        Step = GameObject.Find("Steps");
        Walls = GameObject.FindGameObjectsWithTag("Things");
        this.audioSource = GetComponent<AudioSource>();

        wallNum = 0;
    }

    private void Update()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");
        Gnum = m.Length;
        if (Gnum == 0) 
        {
            foreach (GameObject wall in Walls)
            {
                wall.transform.rotation = Quaternion.Slerp(
                wall.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.time * 0.005f);
                if (check == 0) StartCoroutine(playAudioCo());
            }
        }
        if (check != 0) StopCoroutine(playAudioCo());
        if (check_p != 0) StopCoroutine(popUpAudioCo());

        if (isTimerOn)
        {
            time += Time.deltaTime;
            if (first == true)
                StartCoroutine(popHowTo2());
            else
                StartCoroutine(popHowTo());
        }

    }

    private bool first = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Plain" && !first)
        {
            for (int i = 0; i < 5; i++)
                GameObject.Find("Monster").transform.GetChild(i).gameObject.SetActive(true);
            time = 0.0f; isTimerOn = true; check_p = 0;
            first = true;
        }
    }
    IEnumerator popHowTo()
    {
        if (2f < time && time < 7f)
        {
            howTo.SetActive(true);
            if (check_p == 0)
                StartCoroutine(popUpAudioCo());

        }
        else if (time > 7f)
        {
            howTo.SetActive(false);
            isTimerOn = false;
            StopCoroutine(popHowTo());
        }
        yield return null;
    }

    IEnumerator popHowTo2()
    {
        if (2f < time && time < 7f)
        {
            howTo.SetActive(true);
            howTo.GetComponent<Text>().text = "몬스터는 벽을 통과하니, 조심하세요!\n그리고 무기를 얻으면 1,2,3 키를 눌러보세요.";
            if (check_p == 0)
                StartCoroutine(popUpAudioCo());

        }
        else if (time > 7f)
        {
            howTo.SetActive(false);
            isTimerOn = false;
            StopCoroutine(popHowTo2());
        }
        yield return null;
    }
    IEnumerator playAudioCo()//fence
    {
        audioSource.clip = audioFenceOpen;
        audioSource.PlayOneShot(audioFenceOpen);
        check++;
        yield return null;
    }
    IEnumerator popUpAudioCo()//사용설명서 pop up
    {
        audioSource.clip = audioInform;
        audioSource.PlayOneShot(audioInform);
        check_p++;
        yield return null;
    }
}
