using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public bool isChase;
    Material mat;
    NavMeshAgent nav;

    Rigidbody rigid;
    BoxCollider boxCollider;
    Animator anim;
    GameObject coin;
    public AudioClip audioEnemyDie;
    AudioSource audioSource;


    void Awake()//초기화
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren <Animator>();
        coin = GameObject.Find("2nd").transform.GetChild(3).gameObject;
        this.audioSource = GetComponent<AudioSource>();


        Invoke("ChaseStart", 1);
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }
    void Update()
    {
        if(isChase)
            nav.SetDestination(target.position);        
    }

    void FreezeVelocity() //적이 AI로 날 따라오다 닿았을때 적의 방향 바뀌지 않게 하기 위함 (김보현)
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        FreezeVelocity();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }

        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);//적에 닿는순간 총알 안보이게 하기~ 관통하면 안되니까
            StartCoroutine(OnDamage(reactVec));

        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
            mat.color = Color.white;

        else
        {//죽었을 때
            
            mat.color = Color.grey;
            gameObject.layer = 14;//EnemyDead로 바꿔
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");
            audioSource.clip = audioEnemyDie;
            audioSource.Play();

            reactVec = reactVec.normalized;//값 1로 통일
            reactVec += Vector3.up*1;
            rigid.AddForce(reactVec *4, ForceMode.Impulse);
            if (this.name == "coinMonster")
            {
                coin.transform.position = this.transform.position;
                coin.SetActive(true);
                Debug.Log(coin.name);
            }
            Destroy(gameObject, 0.4f); //1초 뒤에 사라짐
        }
    }
}
