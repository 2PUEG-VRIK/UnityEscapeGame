using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class theCubes : MonoBehaviour
{
    public int value;
    Rigidbody rigid;
    SphereCollider sphere;
    Ray ray;
    RaycastHit hit;
    private theCubes _obj;//���콺�� ������ ť��
    public float rotationSpeed;
    private int count = 0;
    private GameObject clickObject;
    private Vector3 camPos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
     

    private void Update()
    {
        this.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        if (true == Input.GetMouseButtonDown(0))//���콺 ����������
        {
            if (Physics.Raycast(ray, out hit))
            {
                _obj = hit.transform.GetComponent<theCubes>();//_obj�� ������ ť���� ����
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")//���̶� ������
        {
            if (this.name == "Cube" || this.name == "_Cube")
            {
                this.transform.position += new Vector3(0, 0.1f, 0);
                Debug.Log("����");   
                //this.transform.position = new Vector3(this.transform.position.x, this.transform.localPosition.y + 0.1f,
                //    this.transform.position.z);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphere.enabled = false;
        }
    }

    private void OnMouseDrag()
    {
        // �̰� z���� ī�޶��� z�� �������� ���� ����.
        camPos = GameObject.Find("Man Cam").transform.position;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camPos.z + 130);
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        //transform.position = new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x , Camera.main.ScreenToWorldPoint(mousePosition).y, 50);

    }

    //private void OnMouseUp()
    //{
    //    _obj = null;
    //}
}
