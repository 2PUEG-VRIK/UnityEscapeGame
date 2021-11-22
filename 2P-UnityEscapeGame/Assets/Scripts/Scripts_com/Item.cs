using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type
    {
        Weapon, Coin, Heart, Ammo, Key
    };
    //필요에 의해 Heart 추가(김보현)

    public Type type;
    public int value;


    Rigidbody rigid;
    SphereCollider sphereCollider;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        // 이렇게 하면 첫번째 콜라이더를 가져옴 암묵적으로. 이건 선택못하나봄 ㅇㅁㅇ 직접 순서 바꿔야대
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 25 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }
}