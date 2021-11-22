using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    public Transform manPos;

    bool istoWALL = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            istoWALL = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            istoWALL = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        if (!istoWALL)
        {
            if (other.gameObject.tag == "Player")
            {
                transform.position += manPos.forward * 25 * 0.75f * Time.deltaTime;
            }
        }
        else
        {
            rigid.velocity = manPos.forward * 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        if (!istoWALL)
        {
            if (other.gameObject.tag == "Player")
            {
                rigid.velocity = manPos.forward * -0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        if (!istoWALL)
        {
            if (other.gameObject.tag == "Player")
            {
                rigid.AddForce(manPos.forward * -15, ForceMode.VelocityChange);
            }
        }
        else
        {
            rigid.velocity = manPos.forward*0;
        }
    }
}