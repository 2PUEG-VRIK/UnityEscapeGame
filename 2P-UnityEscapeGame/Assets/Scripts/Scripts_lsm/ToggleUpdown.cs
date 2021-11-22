using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUpdown : MonoBehaviour
{
    public GameObject[] targets;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && targets.Length > 0)
        {
            foreach (GameObject target in targets)
            {
                Updownable ud = target.GetComponent<Updownable>();
                if ( ud != null)
                {
                    ud.Toggle();
                }
            }
        }
    }
}
