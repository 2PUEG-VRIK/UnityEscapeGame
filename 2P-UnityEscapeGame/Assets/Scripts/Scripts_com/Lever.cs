using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class Lever : MonoBehaviour
{
    public GameObject gObject;

    bool isLever;

    void Start()
    {

    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isLever && gObject.transform.position.y < 0)
        {
            gObject.transform.position += Vector3.up * 10 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            //Vector3 rotate;
        }


        float xx = other.gameObject.transform.position.x - this.transform.position.x;
        float zz = other.gameObject.transform.position.z - this.transform.position.z;

        if (Mathf.Abs(xx) > Mathf.Abs(zz)) // x값이 더 차이 많이 나면 z를 기울여
        {
            if (xx > 0) // 
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -20));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 20));
            }
        }
        else
        {
            if (zz > 0) // 
            {
                transform.rotation = Quaternion.Euler(new Vector3(20, 0, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(-20, 0, 0));
            }
        }

        isLever = true;
    }

    private void OnTriggerStay(Collider other)
    {
        Move();
    }

    private void OnTriggerExit(Collider other)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        isLever = false;

    }
     
}
