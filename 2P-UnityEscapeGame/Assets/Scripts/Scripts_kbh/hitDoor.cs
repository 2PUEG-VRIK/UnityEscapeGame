using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hitDoor : MonoBehaviour
{
    public int mh; // 문 원래 체력
    public int ch; // 문 현재 체력
    public int value;
    Material mat;
    Material pre;//원래 거 저장하는 변수
    Rigidbody rigid;
    BoxCollider boxcollider;
    BoxCollider PBoxcollider;
    private int doorRotCheck = -1;
    private bool goBack = false;//문 열리면 뒤로 튕기렴
    private GameObject player;
    Vector3 prePlayerPos;//문에 튕기기 전 내 위치
                         //Vector3 offset;//문과 나 사이의 거리
                         //float sqrLen;//거리관련 변수
    public AudioClip audioDoorOpen;
    AudioSource audioSource;
    int check = 0;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        PBoxcollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        pre = GetComponentInChildren<MeshRenderer>().material;
        player = GameObject.FindWithTag("Player");
        check = 0;
        this.audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (doorRotCheck == 1)
            StartCoroutine(doorRotation());

        //if (this.value == 2)
        //{
        //    offset = player.transform.position - this.transform.position;
        //    sqrLen = offset.sqrMagnitude;

        //    if (sqrLen < 625)//거리가 좁을떄만~
        //        goBack = true;
        //}
        prePlayerPos = player.transform.position;
        if (check != 0)
            StopCoroutine(playAudioCo());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && this.value == 0)
        {
            Weapon weapon = other.GetComponent<Weapon>();
            ch -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Debug.Log("아프다는 ㅜ");
            check = 0;
            StartCoroutine(OnDamage());

        }

        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet.damage == 10 && value == 1)
            {
                ch -= bullet.damage;
                check = 0;

                StartCoroutine(OnDamage());
            }

            else if (bullet.damage != 10 && value == 2)
            {
                ch -= bullet.damage;
                check = 0;

                StartCoroutine(OnDamage());
            }

            Destroy(other.gameObject);//적에 닿는순간 총알 안보이게 하기~ 관통하면 안되니까
        }

        boxcollider = this.gameObject.GetComponent<BoxCollider>();
        PBoxcollider = this.gameObject.transform.parent.GetComponent<BoxCollider>();
    }
    IEnumerator OnDamage()
    {
        goBack = true;

        //mat.color = Color.grey;
        yield return new WaitForSeconds(1f);
        mat.color = pre.color;

        if (ch <= 0)
        {   //문 체력 다 닳음
            doorRotCheck = 1;
        }
    }

    IEnumerator doorRotation()
    {
        switch (value)
        {
            case 0: //노란문
                    //234, 3,-22
                if (goBack)
                    player.transform.position = Vector3.Lerp(
                         prePlayerPos,
                         new Vector3(234f, 3f, -22f), Time.deltaTime * 2); //사람이동시켜

                this.transform.rotation = Quaternion.Slerp(
                         this.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.deltaTime); // 문열어
                if(check==0)
                    StartCoroutine(playAudioCo());
                if (player.transform.position.x <= 240f)
                    goBack = false;
                // AudioSource.Destroy(this);
                PBoxcollider.enabled = false;
                boxcollider.enabled = false;
                break;

            case 1: //초록문
                    //286,3,-103
                if (goBack)
                    player.transform.position = Vector3.Lerp(
                         prePlayerPos,
                         new Vector3(286f, 3f, -103f), Time.deltaTime * 2); //사람이동시켜

                this.transform.rotation = Quaternion.Slerp(
                         this.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.deltaTime); // 문열어
                if (check == 0)
                    StartCoroutine(playAudioCo());
                if (player.transform.position.x <= 292f)
                    goBack = false;
                PBoxcollider.enabled = false;
                boxcollider.enabled = false;

                break;

            case 2:
                //if (goBack)
                //    player.transform.position = Vector3.Lerp(
                //         prePlayerPos,
                //         new Vector3(388f, 3f, 31f), Time.deltaTime * 2); //사람이동시켜
                if (check == 0)
                    StartCoroutine(playAudioCo());
                this.transform.rotation = Quaternion.Slerp(
                         this.transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), Time.deltaTime); // 문열어
                PBoxcollider.enabled = false;
                boxcollider.enabled = false;
                break;
        }
                yield return null;
    }
    IEnumerator playAudioCo()
    {
        audioSource.clip = audioDoorOpen;
        audioSource.PlayOneShot(audioDoorOpen);
        check++;
        yield return null;
    }
}

