using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject door;
    ParticleSystem particle;
    Material mat;

    bool isPushed;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.gameObject.SetActive(false);
        mat = GetComponent<MeshRenderer>().material;
        isPushed = false;
    }
    private void OnTriggerStay(Collider other)
    {
        // 꼭 플레이어가 누를 필요없음. 그냥 뭐가 누르고 있으면 문 열어
        // 3-4에서 뭔가에 계속 눌려 있어서, 플레이어나 박스로 지정함
        if (other.tag == "Player" || other.tag == "Box" || other.tag == "Boxsj")
        {
            // 닿는 순간 버튼 눌린 액션
            if (!isPushed)
            {
                transform.position += new Vector3(0, -0.5f, 0);
                isPushed = true;
                mat.color = Color.yellow;

                particle.gameObject.SetActive(true);

                if (!door.GetComponent<Door>().isOpen)
                {
                    door.transform.position += Vector3.up * 15;
                    door.GetComponent<Door>().isOpen = true;
                }
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPushed)
        {
            transform.position += new Vector3(0, +0.5f, 0);
            isPushed = false;
            mat.color = Color.white;

            if (door.GetComponent<Door>().isOpen)
            {
                door.transform.position += Vector3.up * -15;
                door.GetComponent<Door>().isOpen = false;
            }
            particle.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }
}
