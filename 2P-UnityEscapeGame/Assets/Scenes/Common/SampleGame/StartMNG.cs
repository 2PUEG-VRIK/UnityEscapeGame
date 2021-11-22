using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMNG : MonoBehaviour
{
    //public InputField playerNM;
    //public GameObject notDestroy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Stage_Start()
    {

        //notDestroy.GetComponent<SingleGameMNG>().playername = playerNM.text;
        //createFolder(playerNM.text);

        SceneManager.LoadScene("Stage01");
    }

    private void createFolder(string playername)
    {
        // ����� �̸����� ���� �����ϰ� sum 0.0���� �ʱ�ȭ
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("Times").Child(playername).Child("sum").SetValueAsync(0);
    }
}