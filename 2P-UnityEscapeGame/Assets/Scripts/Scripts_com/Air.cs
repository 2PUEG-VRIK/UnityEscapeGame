using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    // 에어펌프(?) 사용자 올라가면 위로 띄워줌.
    // 이때 몸무게 때문에 여자는 되는데 남자는 안되게 힘 조절

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>();

            rigid.AddForce(Vector3.up * 300, ForceMode.Impulse);
        }
    }
}
