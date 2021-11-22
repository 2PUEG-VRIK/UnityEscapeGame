using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gr_Button : MonoBehaviour
{
    // 초록버튼 눌렸을 때
    // (1) 플레이어가 박스를 밀던 방향으로 박스 빠르게 이동
    // (2) 플레이어가 움직이던 방향으로 플레이어 빠르게 이동
    // 결론 : 둘 다 플레이어가 움직이던 방향으로 이동시켜주면 됨

    public Transform gr_buttonPos;     // 플레이어가 움직이던 방향 가져오기 위한 empty. 플레이어안에 있으니 다른 때에 필요하면 이거 넣어서 쓰면 됨.
    Material mat;


    public bool isPushed;

    void Start()
    {
        //mat = GetComponent<MeshRenderer>().material;
        isPushed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPushed)
        {  
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box" || other.gameObject.tag == "Boxsj")       
            {
                transform.position += new Vector3(0, -0.5f, 0);
                isPushed = true;

                Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
                rigid.AddForce(gr_buttonPos.forward * 50, ForceMode.VelocityChange);
                // rigid.velocity = gr_buttonPos.forward * 30;
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPushed)
        {
            transform.position += new Vector3(0, 0.5f, 0); 
            isPushed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }
}
