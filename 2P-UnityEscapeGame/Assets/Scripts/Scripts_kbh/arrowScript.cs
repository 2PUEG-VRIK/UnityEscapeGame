using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowScript : MonoBehaviour
{
    Transform transform;
    private bool check;// 내려가는가~
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)//내려가라 (이게 시작)
            StartCoroutine(goDown());
        else if(!check)

            StartCoroutine(goUp());
    }

    IEnumerator goDown()
    {
        this.transform.Translate(new Vector3(0,-0.1f,0)); //지금 position.y는 -18f
        yield return new WaitForSecondsRealtime(0.25f);
        //if (this.transform.position.y <= -21f)
            check = false; //이제 올려보내
    }

    IEnumerator goUp()
    {
        this.transform.Translate(new Vector3(0, 0.1f, 0));
        //  if (this.transform.position.y >= -18.1f)
        yield return new WaitForSecondsRealtime(0.25f);
            check = true; //이제 내려보내
    }
}
