using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followMeDuck : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    gameManager3 manager;
    public GameObject target;
    void Start()
    {
        manager = GameObject.FindWithTag("Player").gameObject.GetComponent<gameManager3>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.check == 5 && manager.followDuck)//������ ��ȭ �� �������� ~ �� ����;���!
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            Debug.Log("���󰩴ϴ�");
        }
    }
}
