using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_sj : MonoBehaviour
{
    private bool isMove;
    private float speed;

    float hAxis;
    float vAxis;
    Vector3 moveVec;
   

    // Start is called before the first frame update
    void Start()
    {
        isMove = false;        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
    }

    void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical"); 
    }

    private void Move()
    {
        if(isMove)
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            transform.position += moveVec * speed * 0.5f * Time.deltaTime;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMove = true;
            speed = collision.gameObject.GetComponent<Man>().speed;
        }   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMove = false;
            speed = collision.gameObject.GetComponent<Man>().speed;
        }
    }
}
