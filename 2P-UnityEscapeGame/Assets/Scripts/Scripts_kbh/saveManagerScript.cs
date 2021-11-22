using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class saveManagerScript : MonoBehaviour
{

    //string fullpth = "Assets/Resources/kbh/mapData";
    //StreamWriter sw;
    gameManager3 manager;
    //public bool twice = false;//맵 이동 한번했다가 원상태로 온 경우
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
        if (j.yes)//arr에있던 수들 각 변수에 넣어
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


        //if (manager.saveData)//저장해야징
        //{
        //    while (manager.que.Count > 0)
        //    {
        //        sw.WriteLine(manager.que.Dequeue() + ",", false);//false값은 append에 대한 값
        //    }

        //    sw.Flush(); sw.Close();
        //}


    }
}
