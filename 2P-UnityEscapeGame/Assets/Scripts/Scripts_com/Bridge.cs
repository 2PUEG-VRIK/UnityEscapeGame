using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public float Weight;

    public GameObject broken1;
    public GameObject broken2;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject gameObject = collision.gameObject;
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

            if (rigidbody.mass > Weight)
            {
                this.gameObject.SetActive(false);
                broken1.SetActive(true);
                broken2.SetActive(true);
            }
        }

        
    }
}
