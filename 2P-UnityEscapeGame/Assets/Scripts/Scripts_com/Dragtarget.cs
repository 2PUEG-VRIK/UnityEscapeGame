using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragtarget : MonoBehaviour
{
    public GameObject wantObject;
    SimpleCollectibleScript script;

    public bool inTarget;
    void Start()
    {
        script = wantObject.GetComponent<SimpleCollectibleScript>();
        inTarget = false;
	}

    void Update()
    {
        
    }
	 
	private void OnMouseEnter()
	{
        Debug.Log("목표지점 OnMouseEnter");
        if (script.isClicked)
        {
            Debug.Log("script.isClicked");
            Destroy(wantObject);
            inTarget = true;
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("목표지점 OnMouseExit");
 	}

}
