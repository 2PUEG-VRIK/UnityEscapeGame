using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxhurt : MonoBehaviour
{
    public int HP;
    // Start is called before the first frame update
    void Start()
    {
        HP = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            HP--;
            Debug.Log("¸ÁÄ¡ÇÑÅ× ¸Â¾ÒÀ¾´Ï´Ù  "+ HP);
        }
    }
}
