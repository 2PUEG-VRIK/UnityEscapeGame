using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)//¶¥ÀÌ¶û ´êÀ¸¸é
            this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y+ 0.1f,
                this.transform.position.z);
    }
}
