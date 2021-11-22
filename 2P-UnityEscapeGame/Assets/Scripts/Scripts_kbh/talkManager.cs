using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public int length;
    public int value;//npc의 고유번호
    gameManager3 v;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();

        GenerateData();
        //CheckLength(value);
    }
    private void Start()
    {
    }
    private void Update()
    {
    }

    void GenerateData()
    {
        //index, string
        //1.양 -처음
        talkData.Add(1, new string[]
        {  "넌 누구야? 어린애가 밤에 여기까지 무슨일이야?","에구.. 많이 무섭겠다. 그치만 걱정하지마! 여기 사는 동물중에선 두더지가 가장 똑똑하거든. \n두더지를 찾아가면, 집으로 가는 방법을 알 수 있을거야"});

        //2.두더징 -세번째
        talkData.Add(2, new string[]
         {  "뭐야! 내가 더 놀랐어!", "그보다 너, 통로의 돌 때문에 망치를 찾아야하지?","땅 속에서도 다 울리거든!\n흠흠, 나가고싶으면 저 가로등으로 걸어들어가",
         "정말이야~ 더 늦기전에 집에 가야지~\n얼른 들어가!"
        });

        //3. 아파트 옆 못난이 꽃 -두번째
        talkData.Add(3, new string[]
            { "응~ 너 뭐 찾고있는거야??", "나도 나가는 길 어딘지 아는데.\n바로 옆에 아파트 있지? 거기로 가면 돼",
                "(ㅎㅎ재밌당)","미안~\n이제 제대로 알려줄게 ㅎㅎ 여기로 쭉 왼쪽으로 가면~ 숲이 있는데 길 따라 가면 돼!", "그럼~ 이번엔 정말이야 ㅎㅎ 아깐 장난이었어. 미안!"
            });

        //4. 오리 -네번째
        talkData.Add(4, new string[]{
            "흠, 날 찾는구나? 헤헤", "응! 난 고장난 것은 뭐든지 고칠 수 있어~","앗... 문이 고장났어? 고치려면 망치가 필요한데!",
            "걱정 마! 저~기 위에 자동차 보이지? 거기에 가서 두더지를 찾아!","큼큼,, 창피하지만" +
            "난 날지 못해서 그 물체들을 잡을 수 없어..=///=","와~ 찾아왔네?? 지금 고쳐줄게!"
        });


    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }

    public int CheckLength(int value)
    {

        value = GameObject.Find("Man").GetComponent<gameManager3>().value;

        return length = talkData[value].Length;
    }
}