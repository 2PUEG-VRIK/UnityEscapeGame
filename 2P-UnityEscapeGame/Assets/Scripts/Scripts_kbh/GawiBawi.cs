using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
        

public class GawiBawi : MonoBehaviour
{
    Texture2D texture;
    MeshRenderer rend;
    public Image imgs;//���������� ���� �迭 
    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<MeshRenderer>();
        imgs = this.GetComponent<Image>();
        //SystemIOFileLoad();
        rend.material.mainTexture = Resources.Load("kbh/1-3/total.png") as Texture;
       // ImageLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ImageLoad()
    {
        string PATH = "kbh/1-3/total.png";

        this.GetComponent<Image>().sprite = Resources.Load(PATH, typeof(Sprite)) as Sprite;
    }
    //private void ResourceLoad()//ť�꿡~ ���ٺ� ���� �ҷ�����
    //{
    //    Texture2D texture = new Texture2D(0, 0);
    //    string PATH = "kbh/1-3/total.png";
    //    texture = Resources.Load(PATH, typeof(Texture2D)) as Texture2D;//�̹��� �ε�
    //    this.rend.material.mainTexture = texture;

    //    this.GetComponent<Image>().sprite = Resources.Load(PATH, typeof(Sprite)) as Sprite;
    //}

    //private void SystemIOFileLoad()
    //{
    //    string path = @"total.png";
    //    byte[] byteTexture = System.IO.File.ReadAllBytes(path);
    //    if(byteTexture.Length>0)
    //    {
    //        Debug.Log(byteTexture);
    //        texture = new Texture2D(0, 0);
    //        texture.LoadImage(byteTexture);
    //    }

    //    foreach(byte s in byteTexture)
    //    {
    //        Debug.Log(s);
    //    }
    //}
}
