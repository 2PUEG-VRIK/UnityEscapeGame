using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseGawi : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public int value;//���������� ���� ���� ��
    chooseGawi pick;//���� �� ��!

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (true == Input.GetMouseButtonDown(0))//���콺 ����������
        {
            Pick();
        }
    }

    private void Pick()
    {
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("���콺�����Ӽ���222");
            pick = hit.transform.GetComponent<chooseGawi>();//���� pick�� ���� ���� ť�갡 �Ǵ°���(������߿�~)
            Debug.Log(pick.name);
        }
    }
}


