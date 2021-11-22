using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseGawi : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public int value;//가위바위보 승패 결정 값
    chooseGawi pick;//내가 고른 애!

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (true == Input.GetMouseButtonDown(0))//마우스 내려갔나용
        {
            Pick();
        }
    }

    private void Pick()
    {
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("마우스내려왓서요222");
            pick = hit.transform.GetComponent<chooseGawi>();//이제 pick은 내가 누른 큐브가 되는거임(가위방뷔오~)
            Debug.Log(pick.name);
        }
    }
}


