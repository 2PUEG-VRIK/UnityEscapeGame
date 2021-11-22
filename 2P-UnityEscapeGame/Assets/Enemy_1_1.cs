using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy_1_1 : MonoBehaviour
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
    public AudioClip audioEnemyDie;
    AudioSource audioSource;


    void Awake()//�ʱ�ȭ
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
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
        if (isChase)
            nav.SetDestination(target.position);
    }

    void FreezeVelocity() //���� AI�� �� ������� ������� ���� ���� �ٲ��� �ʰ� �ϱ� ���� (�躸��)
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
        if (other.tag == "Weapon" && other.name=="Weapon Hammer")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Debug.Log("����!");
            StartCoroutine(OnDamage(reactVec));
        }

        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Debug.Log("����!");

            Destroy(other.gameObject);//���� ��¼��� �Ѿ� �Ⱥ��̰� �ϱ�~ �����ϸ� �ȵǴϱ�
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
        {//�׾��� ��

            mat.color = Color.grey;
            gameObject.layer = 14;//EnemyDead�� �ٲ�
            isChase = false;
            nav.enabled = false;
            audioSource.clip = audioEnemyDie;
            audioSource.Play();
            anim.SetTrigger("doDie");

            reactVec = reactVec.normalized;//�� 1�� ����
            reactVec += Vector3.up * 1;
            rigid.AddForce(reactVec * 4, ForceMode.Impulse);
            Destroy(gameObject, 0.4f); //1�� �ڿ� �����
            //����� �� �ڸ��� ������ �ϳ� �־��ֱ�
        }
    }
}
