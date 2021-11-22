using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("사라져라!");
        collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("생겨라!");
        collision.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triger 사라져라!" + other.name);
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Triger 생겨라!");
        other.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
