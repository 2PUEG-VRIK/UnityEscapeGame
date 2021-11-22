using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waman : MonoBehaviour
{
    public float speed = 20.0f;
    public float jumpPower = 30.0f;

    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Vector3 preVec;

    Animator anim;
    Rigidbody rigid;

    bool isSwap; // 스왑할땐 아무런 뭣도 안하도록 함.
    bool isBump;
    bool isJump;
    bool sDown1;        //무기바꾸는 변수
    bool sDown2;
    bool sDown3;
    bool iDown;
    bool jDown;
    bool isBorder;      // 벽 통과 못하게 막는 플래그      

    // 무기 부분
    public GameObject[] weapons; // 이게 손에 들려있는 가려진 무기
    public bool[] hasWeapons;
    Weapon equipWeapon; // 무기의 스크립트를 가져오겠다는 거임.
    //장착중인 weapon

    int equipWeaponIndex = -1;

    public int health;
    public int maxHealth;

    public int ammo;
    public int maxAmmo;

    bool AttackDown; // 공격키
    GameObject nearObject;

    bool isFireReady = true;
    float fireDelay;

    void Start()
    {
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Attack();
        Swap();
        Interaction();
    }
    private void FixedUpdate()
    {
        FreezeRotation(); // 플레이어가 탄피나 그런거에 닿으면 회전을 하기 시작.. 그거 없애려고 해주는것임
        StoptoWall();       // 벽 or 박스 통과 방지
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StoptoWall()
    {
        // 2021-09-27 원종진 수정
        // 플레이어에서 길이 3만큼의 Raycast 쐈을 때 Wall 레이어와 닿으면 isBorder ON
        isBorder = Physics.Raycast(transform.position, transform.forward, 3, LayerMask.GetMask("Wall"));
    }

    void GetInput()
    {
        hAxis = Input.GetAxis("HorizontalW");
        vAxis = Input.GetAxis("VerticalW");
        jDown = Input.GetButtonDown("JumpW");

        //   iDown = Input.GetButtonDown("Interaction");
        AttackDown = Input.GetButtonDown("AttackW");
        sDown1 = Input.GetButtonDown("Swap1W");
        sDown2 = Input.GetButtonDown("Swap2W");
        sDown3 = Input.GetButtonDown("Swap3W");
    }

    void Move()
    {
        if (isBump || isSwap)
        {
            return;
        }

        if (!isFireReady)
            moveVec = Vector3.zero;

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isJump)
        {
            moveVec *= 0.5f;
        }

        if (moveVec != Vector3.zero)
        {
            preVec = moveVec;
        }

        if (!isBorder)       // Wall Layer과 충돌하지 않을 때만 이동 가능하게 설정
            transform.position += moveVec * speed * 1f * Time.deltaTime;

        anim.SetBool("isWalk", (moveVec != Vector3.zero));  // 속도가 0이 아니면 걸어라.
    }

    void Turn()
    {
        // 가는 방향 보기.
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        // 점프 키 눌렀을 때 아이템 있으면 아이템 먹음.
        if (jDown)
        {
            if (nearObject != null)
            {
                // 아이템 먹기
                Destroy(nearObject);

                equipWeapon = weapons[equipWeaponIndex].GetComponent<Weapon>();//여기 한번 보기..------------------
                equipWeapon.gameObject.SetActive(true);
                equipWeapon.init();

                //anim.SetTrigger("Swap");
                //isSwap = true; Invoke("SwapOut", 0.5f);

            }
            else if (!isJump)
            {
                // 점프는 그냥 위로 속도주기.
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); //anim.SetBool("isJump", true);
                
                anim.SetTrigger("Jump");
                isJump = true;
            }
        }
    }

    void Swap()
    {

        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if ((sDown1 || sDown2 || sDown3))//점프할때 금지되어있었음
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("Swap");
            isSwap = true;

            Invoke("SwapOut", 0.5f);

        }
    }

    void SwapOut()
    {
        //speed *= 0.5f;
        isSwap = false;
    }

    void Attack()
    {
        if (equipWeapon == null)
        {
            return;
        }

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay; // 공격속도보다 파이어딜레이가 크면 된다고..?

        if (AttackDown && isFireReady && !isSwap)
        {
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "Swing" : "Shot");
            fireDelay = 0; //다음 공격까지 기다리도록
        }
    }

    void Interaction()
    {
        if (nearObject != null) //&&iDown
        {
            if (nearObject.tag == "Item")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex;
                if (item.type == Item.Type.Weapon)
                {
                    weaponIndex = item.value;
                    hasWeapons[weaponIndex] = true;
                }

                // Destroy(nearObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Weapon:
                    nearObject = other.gameObject;
                    break;

                case Item.Type.Coin:
                    Destroy(other.gameObject);
                    this.transform.localScale *= 2;
                    break;

                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;

                case Item.Type.Ammo:
                    ammo += item.value;
                    Debug.Log(ammo);
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;

            }
            Interaction();
            Destroy(other.gameObject);//원래 exit에 있었음

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Weapon:
                    // nearObject = null;
                    break;
                case Item.Type.Ammo:

                    // nearObject = null;
                    break;
                case Item.Type.Heart:

                    //nearObject = null;
                    break;

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("벽이랑 닿았다.!");
        }

        if (collision.gameObject.tag == "Player")
        {
            // 플레이어끼리 부딪히면 튕기기 애니메이션
            Bump();
        }

        // 바닥 닿으면 다시 점프 가능상태로 바꿔주기.
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Box")
        {
            isJump = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {

    }

    void Bump()
    {
        //anim.SetTrigger("Bump");
        //isBump = true;
        //transform.position += preVec * -7;

        //Invoke("BumpOut", 1.5f);
    }

    void BumpOut()
    {
        //isBump = false;
    }


    //void OnGUI()
    //{
    //    //무슨 키 입력했는지 알려주는 코드.
    //    Event e = Event.current;
    //    if (e.isKey)
    //    {
    //        Debug.Log("Detected a keyboard event!" + e.keyCode);
    //    }

    //}
}
