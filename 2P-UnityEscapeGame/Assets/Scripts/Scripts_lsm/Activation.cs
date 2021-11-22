using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  버튼 등에 Activation 스크립트를 넣고,
 *  target에 제어할 대상 GameObject를 넣고, 
 *  제어하고 싶은 대상(target과 동일)에 Activator 스크립트를 넣고,
 *  제어할 스크립트 Update() 함수에 if (!(this.GetComponent<Activator>()?.isOn ?? true)) 문 추가
 */
public class Activation : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Activator a = target.GetComponent<Activator>();
            if (a != null)
            {
                a.isOn = !a.isOn;
            }
        }
    }
}
