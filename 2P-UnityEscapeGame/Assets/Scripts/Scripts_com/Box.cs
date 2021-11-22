using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;

    float speed;
    bool force_move = false;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        // 랜덤으로 돌아다니기.
        //transform.position += new Vector3(Random.Range(0, 20), 0, 0) * Time.deltaTime;
        Move();
        speed = Random.Range(2,5);

    }

    public void Move()
    {
        if (rigid.velocity == Vector3.zero || force_move)
        {
            rigid.velocity = new Vector3(Random.Range(-5, 10), 0, Random.Range(-5, 10)) * speed;
            force_move = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Move();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            force_move = true;
        }
    }

    private void OnTriggerEnter(Collider other) // 공격 당한걸 트리거가 닿았는지로 판단할거니까 총알이랑 망치에다가 isTrigger 체크해줘야댐 콜라이더에
    {
        if (other.tag == "Ball")
        {
            Item ball = other.GetComponent<Item>();
            curHealth -= 10;
            
            Destroy(other.gameObject);
            StartCoroutine(OnDamage());
        }
    }
     

    IEnumerator OnDamage()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.color = Color.white;
        }
        else // 죽었을 때
        {
            Destroy(gameObject, 2);
            StartCoroutine(LoadSceneCorutine());
        }
    }

    IEnumerator LoadSceneCorutine()
    {
        yield return SceneManager.LoadSceneAsync("Scene2", LoadSceneMode.Single);
    }
}
