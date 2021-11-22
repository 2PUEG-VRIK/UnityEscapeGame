using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };//m은 근거리, r은 원거리
    public Type type;
    public int damage;
    public float rate;
    private int curAmmo;

    public BoxCollider meleeArea;//공격 범위
    public TrailRenderer trailEffect;//휘두를 때 나타나는 효과
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;
    public AudioClip audioSwing;
    AudioSource audioSource;


    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        curAmmo = GameObject.Find("Man").GetComponent<Man>().ammo;
    }
    public void init()
    {
        trailEffect.enabled = false;
    }
    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }

        else if (type == Type.Range && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }

        else if (type == Type.Range && curAmmo < 0)
            curAmmo = 0;
    }
    IEnumerator Swing()
    {
        audioSource.clip = audioSwing;
        audioSource.Play();
        trailEffect.enabled = true;

        //yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;


        yield return new WaitForSeconds(1f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.6f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        //1. 총알발사 (함수로 총알 인스턴스화 하기)
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();//인스턴스화 된 총알에 속도 적용하기
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null; // 한 프레임 쉬어

        //2. 탄피 배출
        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = instantCase.GetComponent<Rigidbody>();//인스턴스화 된 총알에 속도 적용하기
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);//즉각적인
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);//회전축은 마음대로
    }
}