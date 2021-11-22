using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updownable : MonoBehaviour
{
    public bool isUp = true;

    public void Toggle()
    {
        if (isUp)
        {
            this.transform.position -= this.transform.lossyScale;
        }
        else
        {
            this.transform.position += this.transform.lossyScale;
        }
        isUp = !isUp;
    }
}
