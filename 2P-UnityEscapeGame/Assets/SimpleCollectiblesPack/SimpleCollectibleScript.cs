using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {Key, ClickItem, DragItem, Type4, Type5}; // you can replace this with your own labels for the types of collectibles in your game!

	public CollectibleTypes CollectibleType; // this gameObject's type

	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;


	// 클릭시 생성될 오브젝트
	public GameObject rewardObject;

	private Vector3 m_Offset;
	private float m_ZCoord;

	// 드래그해서 원하는 지점에 놓기 
	private Vector3 pre_position;
	// 목표 지점
	public GameObject target;
	private Dragtarget target_script;

 	public bool isClicked;

	// Use this for initialization
	void Start () {
		pre_position = transform.position;
 		isClicked = false;

		if(CollectibleType == CollectibleTypes.DragItem)
        {
			target_script = target.GetComponent<Dragtarget>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World); 
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		} 
	}

    private void OnCollisionEnter(Collision collision)
    {
		if(CollectibleType == CollectibleTypes.Key)
        {
			GetComponent<Rigidbody>().isKinematic = true;
			this.GetComponent<SphereCollider>().isTrigger = true;
		}
		
	}

	public void Collect()
	{
		if (collectSound)
			Debug.Log("collectSound 있긴 함");
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		//Below is space to add in your code for what happens based on the collectible type
		 
		if (CollectibleType == CollectibleTypes.Key) {

			Debug.Log("Do Key Command");
			
			//sample1에 있는 노란 열쇠. 먹으면 캐릭 머리위 활성화
			GameObject player = GameObject.Find("Man").gameObject;
            GameObject key = GameObject.Find("Man").transform.GetChild(1).gameObject;
 
			player.GetComponent<Man>().hasKey = true;
 			key.SetActive(true);
		}
		 
		Destroy (gameObject);
	}

	private void OnMouseDown()
	{
		if (CollectibleType == CollectibleTypes.ClickItem || CollectibleType == CollectibleTypes.DragItem)
        {
			Debug.Log("OnMouseDown");
			m_ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
			m_Offset = gameObject.transform.position - GetMouseWorldPosition();
			isClicked = true;
		}

	}

    private Vector3 GetMouseWorldPosition()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = m_ZCoord;

		return Camera.main.ScreenToWorldPoint(mousePoint);
	}

	private void OnMouseUp()
	{
		if (CollectibleType == CollectibleTypes.ClickItem)
		{
			// 해당 위치에 오브젝트 생성
			Instantiate(rewardObject, transform.position, Quaternion.Euler(0, 0, -90));

			Destroy(gameObject);
		}
		if (CollectibleType == CollectibleTypes.DragItem)
        {
			Debug.Log("느낌표 마우스 UP");
			if (target_script.inTarget)
            {
				// 물건 뽀셧음.
            }
            else
            {
				//제자리로
				Debug.Log("제자리로");
				transform.position = pre_position;
				isClicked = false;
			}
		}
	}
	private void OnMouseEnter()
	{
	}

	private void OnMouseDrag()
	{
		if (CollectibleType == CollectibleTypes.DragItem)
		{
			transform.position = GetMouseWorldPosition() + m_Offset;
		}
	}
}
