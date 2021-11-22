using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonItem : MonoBehaviour 
{
    public GameObject[] items;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && items.Length > 0)
        {
            foreach (GameObject item in items)
            {
                item.SetActive(!item.activeSelf);
            }
        }
    }
}