using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : Character
{
    Rigidbody rb;
    //JoystickControl joystick;
    [SerializeField] protected Joystick joystick;
    private Vector3 movement;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private HatData hatData;
    // [SerializeField] private Transform pointOfWeapon;
    float horizontalInput;
    float verticalInput;

  //  private Vector3 directWeapon;


   // [SerializeField]
    private float timeDelayAttack = 1f;
    private float couter = 0;

    public Enemy targetEnemy;

    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private GameObject curHat;


    public Transform hand;
    public Transform head;


    [SerializeField] private bool canAttack = true;

    [SerializeField] ShopDataCustom shopDataCustom;

    [SerializeField] private List<int> listSkill = new List<int>() {0,0,0,0,0 };
    // [SerializeField] private List<>

    float couterHP = 0;



    // Start is called before the first frame update
    void Start()
    {

        ChangeWeapon(shopDataCustom.weaponsItems[PlayerData.curWeaponID].weaponType);
        ChangeHat(shopDataCustom.hatItems[PlayerData.curHatID].hatType);





        // OnInit();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isdead="+IsDead);
        if (GameManager.Ins.IsState(GameState.GamePlay) && !IsDead)
        {

         //   RecoverHp(10, 1);
            //  Debug.Log(IsDead);

            //if (IsDead)
            //{
            //    OnDespawn();
            //}



            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;


            movement.Set(horizontalInput, 0f, verticalInput);
            movement.Normalize();
            FindTargetEnemy();

            if (movement != Vector3.zero)
            {
                Move();
                //  couter = 0;
                couter += Time.deltaTime;
            }

            if (movement == Vector3.zero)
            {
                if (targetEnemy == null)
                {
                    StopMove();
                    //TO DO:
                }
                if (targetEnemy != null /*&& isAttacking==false*/)
                {
                    //isAttacking = true;
                    LoadAttack();
                    couter += Time.deltaTime;
                    if (-couter + timeDelayAttack < 0.01f)
                    {
                        // Debug.Log("Att");
                        OnAttack();
                        // ChangeAnim(Constant.ANIM_IDLE);
                        couter = 0;
                        //isAttacking = false;



                    }



                }

            }

            //if (hpBar != null)
            //{
            //    hpBar.transform.position = transform.position;
            //    hpBar.hpBarSlide.value = Mathf.Lerp(hpBar.hpBarSlide.value, curHp / maxHp, Time.deltaTime * 5f);
            //    hpBar.textHp.text = curHp.ToString();
              //}
        }





       
        //StopMove();



        // Debug.Log(targetEnemy.gameObject.name);
        // StopMove();


    }

    public override void OnInit()
    {
      //  this.gameObject.SetActive(true);
        base.OnInit();
        rb = GetComponent<Rigidbody>();

        maxHp = 100*RoomManager.Ins.numOfRoom;
        curHp = maxHp;
        //ChangeWeapon(WeaponType.Arrow);
        //ChangeHat(HatType.BarbarianHat);
    }

    public void OnInitReTry()
    {
      //  this.gameObject.SetActive(true);
        base.OnInit();

        maxHp = 100 * RoomManager.Ins.numOfRoom;
        curHp = maxHp;
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public override void OnDespawn()
    {
         Debug.Log("da cvao bat ui");
        if (hpBar != null)
        {
            Destroy(hpBar.gameObject);
        }

        //  this.gameObject.SetActive(false);

        GameManager.Ins.EndGame();
       
    }
    public override void OnDeath()
    {
        IsDeaded = true;
        StopMove();
        ChangeAnim(Constant.ANIM_DIE);
       
        Invoke(nameof(OnDespawn), 2f);
    }
    private void Move()
    {

            rb.velocity = movement * moveSpeed;
            ChangeAnim(Constant.ANIM_RUN);
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f);
        
        //else 
        //{

        //    LoadAttack();
         
        //    //  rb.constraints.

        //}


    }
    private void StopMove()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ChangeAnim(Constant.ANIM_IDLE);
    }

    private void FindTargetEnemy()
    {
        if (RoomManager.Ins.liEnemyInRoom.Count > 0)
        {
            float targetDis = 100f;
            float curDis = 0; 
            for (int i = 0; i < RoomManager.Ins.liEnemyInRoom.Count; i++)
            {
                if (RoomManager.Ins.liEnemyInRoom[i] == null)
                {
                    RoomManager.Ins.liEnemyInRoom.Remove(RoomManager.Ins.liEnemyInRoom[i]);
                }
                else
                {
                    curDis = Vector3.Distance(transform.position, RoomManager.Ins.liEnemyInRoom[i].transform.position);
                    if (targetDis > curDis)
                    {
                        targetDis = curDis;
                        targetEnemy = RoomManager.Ins.liEnemyInRoom[i];
                        // Debug.Log(targetEnemy.gameObject.name+""+curDis+""+targetDis);

                    }
                }

            }

            // return targetEnemy;
        }
        else if(RoomManager.Ins.liEnemyInRoom.Count ==0)
        {
            targetEnemy = null;
        }


    }



    public void ChangeWeapon(WeaponType weaponType)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weaponData.GetWeapon(weaponType), hand);
    }

    public void ChangeHat(HatType hatType)
    {
        if (curHat != null)
        {
            Destroy(curHat) ;
        }
        curHat= Instantiate(hatData.GetHat(hatType),head);
    }

    private void LoadAttack()
    {
        //FindTargetEnemy();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        ChangeAnim(Constant.ANIM_LOADATTACK);
        Vector3 lookAtVector = new Vector3(targetEnemy.transform.position.x, transform.position.y, targetEnemy.transform.position.z);
        transform.LookAt(lookAtVector);
       //    pointOfWeapon.LookAt(targetEnemy.transform);
       // directWeapon = (targetEnemy.transform.position - pointOfWeapon.position).normalized;
        
        // ChangeAnim(null);
        //  ChangeAnim(Constant.ANIM_IDLE);
       
  
        //  OnAttack();
    }
    public void OnAttack()
    {
        //ban dan
       currentWeapon.pointOfWeapon.LookAt(targetEnemy.transform);

        Vector3 rot =new Vector3(currentWeapon.pointOfWeapon.rotation.eulerAngles.x-90, currentWeapon.pointOfWeapon.rotation.eulerAngles.z, currentWeapon. pointOfWeapon.rotation.eulerAngles.y /*directWeapon.y*/);
         Bullet bl= Instantiate(currentWeapon.bullet,currentWeapon. pointOfWeapon.position,Quaternion.Euler(rot));

         // Bullet bl1 = SimplePool.Spawn<Bullet>(PoolType.Bullet, currentWeapon.pointOfWeapon.position, Quaternion.Euler(rot));
        // PoolType
      //  Bullet bl1 = SimplePool.Spawn<Bullet>(currentWeapon.bullet, currentWeapon.pointOfWeapon.position, Quaternion.Euler(rot));
       
        bl.OnInit(currentWeapon.pointOfWeapon.transform.forward);


        //  SimplePool.Despawn(bl1);
        //  bl.gameObject.SetActive(true);
        ChangeAnim(Constant.ANIM_ATTACK);
         //skill


       
    }

    //skill
    // hoi mau theo giay
    private void RecoverHp(float amountOfHp, int timer)
    {
        
       
        

        if (curHp < maxHp*0.8)
        {
            couterHP += Time.deltaTime;
            if (timer - couterHP < 0.01f)
            {

                curHp += amountOfHp;
                couterHP = 0;
            }
        }
        else if(curHp>maxHp)
        {
            curHp = maxHp;
        }


    }







}


