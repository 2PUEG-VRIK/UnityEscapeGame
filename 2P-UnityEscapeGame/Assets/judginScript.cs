using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judginScript : MonoBehaviour
{
    //���ٿԴ��� �ƴ��� �Ǻ��ϴ� script
    // Start is called before the first frame update
    public ArrayList arr1 = new ArrayList();
    saveManagerScript manager;
    gameManager3 gameManager;
    public bool yes = false;
    public bool yes_2 = false;
    void Start()
    {
        manager = GameObject.FindWithTag("Player").GetComponent<saveManagerScript>();
        gameManager = GameObject.FindWithTag("Player").GetComponent<gameManager3>();

    }

    public void saveQue(int x, int y, int z, int c, int v)//check�� value �޴´�
    {
        if (gameManager.check == -1)
        {
            gameManager.check = 2;

            arr1.Add(x);
            arr1.Add(y);
            arr1.Add(z);
            arr1.Add(gameManager.check);
            arr1.Add(v);
            arr1.Add(3);
            arr1.Add(3);

        }
        else if (c == 4)
        {
            arr1.Add(x);
            arr1.Add(y);
            arr1.Add(z);
            arr1.Add(gameManager.check);
            arr1.Add(v);
            arr1.Add(6);//���̶� ��ȭ�̾���ϹǷ� �� index
            arr1.Add(5);//�� index ����
            
            gameManager.check = -4;
        }
        Debug.Log(gameManager.check);

      
    }
}