using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class saveManagerScript : MonoBehaviour
{

    //string fullpth = "Assets/Resources/kbh/mapData";
    //StreamWriter sw;
    gameManager3 manager;
    //public bool twice = false;//�� �̵� �ѹ��ߴٰ� �����·� �� ���
    judginScript j;
    GameObject man;

    // Start is called before the first frame update
    void Start()
    {
        //if (false == File.Exists(fullpth))
        //    sw = new StreamWriter(fullpth + ".txt");

        manager = GameObject.Find("Man").GetComponent<gameManager3>();
        man = GameObject.FindWithTag("Player");
        j = GameObject.Find("judging").GetComponent<judginScript>();
        if (j.yes)//arr���ִ� ���� �� ������ �־�
        {
            man.transform.position =new Vector3((int)j.arr1[0]-15, (int)j.arr1[1], (int)j.arr1[2] );
            manager.check = (int)j.arr1[3];
            manager.value = (int)j.arr1[4];
            if(j.yes_2)
                man.transform.position = new Vector3((int)j.arr1[0] , (int)j.arr1[1], (int)j.arr1[2]);
        }

    }

    void Update()
    {


        //if (manager.saveData)//�����ؾ�¡
        //{
        //    while (manager.que.Count > 0)
        //    {
        //        sw.WriteLine(manager.que.Dequeue() + ",", false);//false���� append�� ���� ��
        //    }

        //    sw.Flush(); sw.Close();
        //}


    }
}
