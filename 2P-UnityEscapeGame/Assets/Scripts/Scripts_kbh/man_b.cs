using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class man_b : MonoBehaviour
{

    public float speed = 30.0f;
    public float jumpPower;

    float hAxis;
    float vAxis;
    Vector3 moveVec;
    Vector3 preVec;

    Animator anim;
    Rigidbody rigid;

    bool isSwap;        // �����Ҷ� �ƹ��� ���� ���ϵ��� ��.
    bool isBump;
    public bool isJump;
    public bool sDown1;        //����ٲٴ� ����
    bool sDown2;
    bool sDown3;
    bool iDown;
    bool jDown;
    bool isBox;

    bool istoWALL;
    bool istoObj;

    public bool isGRbtn;

    bool istoDoor;

    bool onStair_up; // ��� �ö󰡴� ����Ű�� ���� ���� �з�
    bool onStair_down;
    bool onStair_right;
    bool onStair_left;

    public Transform fastPos;
    bool isDead = false;    // ���� ����
    bool isBorder;      // �� ��� ���ϰ� ���� �÷���      
    public bool hasKey;

    // ���� �κ�
    public GameObject[] weapons; // �̰� �տ� ����ִ� ������ ����
    public bool[] hasWeapons;
    Weapon equipWeapon; // ������ ��ũ��Ʈ�� �������ڴٴ� ����.
    //�������� weapon

    int equipWeaponIndex = -1;

    public int health;
    public int maxHealth;

    public int ammo;
    public int maxAmmo;

    bool AttackDown; // ����Ű
    GameObject nearObject;  //Ʈ���� �� ������ �����ϴ� ����

    bool isFireReady = true;
    float fireDelay;

    private bool goBack = false;//���� ����� �� �ڷ� ����
    Vector3 playerPos;
    IEnumerator enu1; //ladder�� �ʿ�
    Vector3 prePos;//�ڷ� �����ϱ� �� �÷��̾��� ���� ��ġ
    gameManager3 manager;

    public AudioClip audioGunShot;
    public AudioClip audioJump;
    public AudioClip audioEatItem;
    public AudioClip audioSwap;
    AudioSource audioSource;

    void Start()
    {
        //jumpPower = 150.0f;
        hasKey = false;
        onStair_up = false;
        onStair_down = false;
        onStair_right = false;
        onStair_left = false;
        isBox = false;
        istoWALL = false;
        istoDoor = false;
        manager = this.GetComponent<gameManager3>();
    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        this.audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!isGRbtn)
            GetInput();

        Move();
        Turn();
        Jump();
        Attack();
        Swap();
    }
    private void FixedUpdate()
    {
        FreezeRotation();   // �÷��̾ ź�ǳ� �׷��ſ� ������ ȸ���� �ϱ� ����.. �װ� ���ַ��� ���ִ°���
        StoptoWall();       // �� or �ڽ� ��� ����

    }


    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StoptoWall()
    {
        // 2021-09-27 ������ ����
        // �÷��̾�� ���� 3��ŭ�� Raycast ���� �� Wall ���̾�� ������ isBorder ON

        istoWALL = Physics.Raycast(fastPos.position, transform.forward, 5, LayerMask.GetMask("Wall"));

        //istoWALL = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));

        istoObj = Physics.Raycast(transform.position, transform.forward, 3.5f, LayerMask.GetMask("Box"));
    }

    void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        jDown = Input.GetButtonDown("Jump");
        //   iDown = Input.GetButtonDown("Interaction");
        AttackDown = Input.GetButton("Attack");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

        if (Input.anyKeyDown)
        {
            if (isBox)
            {
                isBox = false;
            }
        }
    }


    void Move()
    {

        if (isBump || isSwap || isDead ||manager.dontMove) //������ �� ����
        {
            return;
        }

        if (!isFireReady)
            moveVec = Vector3.zero;

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isJump || isBox)
        {
            moveVec *= 0.5f;
        }

        if (moveVec != Vector3.zero)
        {
            preVec = moveVec;
        }
        else
        {

        }

        if (onStair_up) // �ö󰡴� �����̴ϱ� �������� ����Ű �϶� y�� ���� �Ʒ��� ��.
        {
            moveVec = new Vector3(hAxis, vAxis * 0.5f, vAxis).normalized;
        }
        if (onStair_down)
        {
            moveVec = new Vector3(hAxis, vAxis * -0.5f, vAxis).normalized;
        }
        if (onStair_right)
        {
            moveVec = new Vector3(hAxis, hAxis * 0.5f, vAxis).normalized;
        }
        if (onStair_left)
        {
            moveVec = new Vector3(hAxis, hAxis * -0.5f, vAxis).normalized;
        }

        //if (istoWALL)       // Wall Layer�� �浹���� ���� ���� �̵� �����ϰ� ����
        //    transform.position += moveVec * 0 * Time.deltaTime;

        //else if (!istoWALL && istoObj)
        //    transform.position += moveVec * speed * 0.375f * 1f * Time.deltaTime;

        //else
        //    transform.position += moveVec * speed * 1f * Time.deltaTime;

        if (!istoWALL && !isDead && !istoDoor)       // Wall Layer�� �浹���� ���� ���� �̵� �����ϰ� ���� 
            transform.position += moveVec * speed * 1f * Time.deltaTime;

        anim.SetBool("isWalk", (moveVec != Vector3.zero));  // �ӵ��� 0�� �ƴϸ� �ɾ��.
    }

    void Turn()
    {
        // ���� ���� ����.
        Vector3 watchVec = new Vector3(moveVec.x, 0, moveVec.z);
        transform.LookAt(transform.position + watchVec);
    }

    void Jump()
    {
        // ���� Ű ������ �� ������ ������ ������ ����.
        if (jDown)
        {
            audioSource.clip = audioJump;
            audioSource.Play();
            if (nearObject != null)
            {
                // ������ �Ա�
                Destroy(nearObject);

                equipWeapon = weapons[equipWeaponIndex].GetComponent<Weapon>();
                equipWeapon.gameObject.SetActive(true);
                equipWeapon.init();
            }
            else if (!isJump)
            {
                // ������ �׳� ���� �ӵ��ֱ�.
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                anim.SetTrigger("Jump");
                isJump = true;
            }
        }
    }
    public void Swap()
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

        if ((sDown1 || sDown2 || sDown3))//�����Ҷ� �����Ǿ��־���
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("Swap");
            audioSource.clip = audioSwap; ;
            audioSource.Play();
            isSwap = true;

            Invoke("SwapOut", 0.5f);

        }
    }

    void SwapOut()
    {
        isSwap = false;
    }

    void Attack()
    {
        if (equipWeapon == null) //���� ���Ⱑ ������
        {
            return;
        }

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay; // ���ݼӵ�(��Ÿ��)���� ���̾������(���� �ð�)�� ũ�� �ȴٰ�..?

        if (AttackDown && isFireReady && !isSwap)
        {
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "Swing" : "Shot");
            if (equipWeapon.type != Weapon.Type.Melee)//���̸� �Ѿ� -1
            {
                audioSource.clip = audioGunShot;
                audioSource.Play();

                ammo--;
                if (ammo <= 0)
                    ammo = 0;
            }
            fireDelay = 0; //���� ���ݱ��� ��ٸ�����
        }
    }

    void Interaction(GameObject sth)
    {
        int weaponIndex;
        if (sth != null)
        {
            Item item = sth.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Weapon:

                    weaponIndex = item.value;
                    hasWeapons[weaponIndex] = true;
                    break;
            }
        }
    }

    void OnDie()
    {
        if (!isDead)
        {
            anim.SetTrigger("doDie");
            isDead = true;
        }
    }

    public int check = -1;//���� ���� ����(�躸��)

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fButton")
        {
            isGRbtn = true;
            rigid.AddForce(fastPos.forward * 30, ForceMode.VelocityChange);
        }

        if (other.tag == "Water")
        {
            OnDie();
        }

        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();

            switch (item.type)
            {
                case Item.Type.Weapon:

                    break;
                case Item.Type.Coin:

                    check = 1;

                    //this.transform.localScale *= 2;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;

                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;
                    //case Item.Type.Key:

            }
            Interaction(other.transform.gameObject);
            audioSource.clip = audioEatItem;
            audioSource.Play();
            Destroy(other.gameObject);
        }

        else if (other.tag == "Enemy")
        {
            health--;
            Bump();
            if (health <= 0)
                Quit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
            goBack = false;

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Wall")
        {
            isGRbtn = false;
        }

        // �ٴ� ������ �ٽ� ���� ���ɻ��·� �ٲ��ֱ�.
        //if (Physics.Raycast(transform.position, -transform.up, 3))
        if (collision.gameObject.layer == 7 || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Boxsj")

        {
            onStair_up = false;
            onStair_down = false;
            onStair_right = false;
            onStair_left = false;
            isJump = false;
        }
        if (collision.gameObject.tag == "StairUp")
        {
            onStair_up = true;
            onStair_down = false;
            onStair_right = false;
            onStair_left = false;
        }
        if (collision.gameObject.tag == "StairDown")
        {
            onStair_up = false;
            onStair_down = true;
            onStair_right = false;
            onStair_left = false;
        }
        if (collision.gameObject.tag == "StairRight")
        {
            onStair_up = false;
            onStair_down = false;
            onStair_right = true;
            onStair_left = false;
        }
        if (collision.gameObject.tag == "StairLeft")
        {
            onStair_up = false;
            onStair_down = false;
            onStair_right = false;
            onStair_left = true;
        }
        if (collision.gameObject.tag == "Boxsj")
        {
            isBox = true;
        }
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Wall")))
        {
            istoWALL = true;
        }
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Door")))
        {
            istoDoor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //{
        //    if (collision.gameObject.tag == "StairUp")
        //    {

        //        onStair_up = false;
        //    }
        //    if (collision.gameObject.tag == "StairDown")
        //    {

        //        onStair_down = false;
        //    }
        //    if (collision.gameObject.tag == "StairRight")
        //    {

        //        onStair_right = false;
        //    }
        //    if (collision.gameObject.tag == "StairLeft")
        //    {

        //        onStair_left = false;
        //    }

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Wall")))
        {
            istoWALL = false;
        }
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Door")))
        {
            istoDoor = false;
        }
    }

    void Bump()
    {
        anim.SetTrigger("Bump");
        isBump = true;
        transform.position += preVec * -10;

        Invoke("BumpOut", 1.5f);
    }

    void BumpOut()
    {
        isBump = false;
    }

    //void OnGUI()
    //{
    //    //���� Ű �Է��ߴ��� �˷��ִ� �ڵ�.
    //    Event e = Event.current;
    //    if (e.isKey)
    //    {
    //        Debug.Log("Detected a keyboard event!" + e.keyCode);
    //    }

    //}

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}