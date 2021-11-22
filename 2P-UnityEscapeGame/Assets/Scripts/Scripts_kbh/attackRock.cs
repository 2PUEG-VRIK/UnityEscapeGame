using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackRock : MonoBehaviour
{
    int attkCount;
    GameObject hinder;
    public bool Final;
    public AudioClip audioAttackRock;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        attkCount = 0;
        hinder = GameObject.Find("hinder");
        Final = false;
    }
    private void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (attkCount == 3)
        {
            hinder.SetActive(false);
            Final = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            attkCount++;
            audioSource.clip = audioAttackRock;
            audioSource.Play();
        }
    }
}
