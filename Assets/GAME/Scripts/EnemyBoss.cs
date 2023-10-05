using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using CameraShake;
using DG.Tweening;

public class EnemyBoss: Enemy
{
    [SerializeField] protected StateMachine stateMachine = new StateMachine();
    [SerializeField] protected float rangeAttack = 15f;
    [SerializeField] protected float rangeSingt = 10f;
    [SerializeField] private float damage = 10f;
    private bool canAttack = true;
    private bool isLoadingAttack= false;
    private bool isAttacking = false;


    [SerializeField] private Transform pointOfBullet;
    [SerializeField] private BulletBoss bulletE;

    BulletBoss bl;




    //  public ParticleSystem vfxE;


    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] AttackArea attackArea;
    [SerializeField] AttackArea attackArea2;
    [SerializeField] GameObject waringCircle;

    GameObject InswaringCircle;
    private float timerSleep;
    private float timerPatrol;
    private float timerAttack;

    private float couter = 0;

    

    private Player player => RoomManager.Ins.player;

 //   public Player player;



    // Start is called before the first frame update
    void Start()
    {
       //  OnInit();


        //  drawRange();
        // OnDrawGizmos();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
          //  OnDeath();
            RoomManager.Ins.liEnemyInRoom.Remove(this);
        }
        else
        {
           stateMachine?.Execute();
        }

        //  hpBar.value = Mathf.Lerp(hpBar.value, curHp / maxHp, Time.deltaTime * 5f);
        //  Debug.Log(hpBar.value);
        //if (hpBar != null)
        //{
        //    hpBar.transform.position = transform.position;
        //    hpBar.hpBarSlide.value = Mathf.Lerp(hpBar.hpBarSlide.value, curHp / maxHp, Time.deltaTime * 5f);
        //}


       


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeSingt);
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
        //  Gizmos.color = Color.red;

    }
    public override void OnInit()
    {
        hpBar = Instantiate(HpBarPrefab, transform.position,HpBarPrefab.transform.rotation);

        hpBar.OnInit(this);
        maxHp = 500 * RoomManager.Ins.numOfRoom;
        curHp = maxHp;
        InswaringCircle= Instantiate(waringCircle, transform.position, waringCircle.transform.rotation);
       

        stateMachine.ChangeState(AttackState1);

    }
    public override void OnDeath()
    {
        StopMove();
        base.OnDeath();

    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(InswaringCircle);
    }

    public void SleepState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            timerSleep = UnityEngine.Random.Range(1f, 2f);
            ChangeAnim("SleepStart");
        };

        onExecute = () =>
        {
            couter += Time.deltaTime;
            if (-couter + timerSleep < 0.01f)
            {
             //   stateMachine.ChangeState(PatrolState);
                couter = 0;
            }

        };

        onExit = () =>
        {
            ChangeAnim("SleepEnd");
        };
    }

    // dung nem da
    public void AttackState1(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        int numOfFire = 2;
        onEnter = () =>
        {
         // agent.isStopped = true;
            timerAttack = 2f;
            rangeAttack = 30f;
            canAttack = true;
            couter = 0;


        };

        onExecute = () =>
        {
            if (player.IsDead == false)
            {

            

                if (Vector3.Distance(transform.position, player.transform.position) < rangeAttack)
                {
                    Debug.Log(canAttack);

                    //  agent.isStopped = true;
                    //  Debug.Log("attackE");
                    // agent.isStopped = true;

                    // thoi gian attack phai nho hon thoi gian giua 2 don danh
                    if (canAttack)
                    {
                        Debug.Log("att1");
                        if (numOfFire > 0)
                        {
                            Attack1();
                            numOfFire--;
                        }
                        else
                        {
                            if (UnityEngine.Random.value < 0.5)
                            {
                                Attack1();
                            }
                            else
                            {
                                stateMachine.ChangeState(AttackState2);
                            }
                        }

                        

                           
                        
                       
                       
                        canAttack = false;
                    }
                    couter += Time.deltaTime;
                    if (timerAttack - couter < 0.01f)
                    {
                        canAttack = true;
                        couter = 0;
                        // agent.isStopped = false;
                    }

                }


            }

        };

        onExit = () =>
        {
            canAttack = true;
            isAttacking = false;
        };
    }

    public void AttackState2(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            canAttack = true;
            agent.stoppingDistance = 4f;
            rangeAttack = 4f;
            timerAttack = 2f;

            couter = 0f;

        };

        onExecute = () =>
        {
            if (player.IsDead == false)
            {

                // Debug.Log(tarpos);
                couter += Time.deltaTime;
                if (Vector3.Distance(transform.position, player.transform.position) < rangeAttack )
                {
                    // Debug.Log(Vector3.Distance(transform.position, player.transform.position) - rangeAttack);
                    // Debug.Log("phaidunglai"+ canAttack+ couter);
                    //  agent.isStopped = true;
                    //  Debug.Log("attackE");
                    // agent.isStopped = true;

                    //if (isAttacking == false)
                    //{
                    //    agent.velocity = Vector3.zero;
                    //}

                 // Debug.Log(canAttack);

                    if (canAttack == true)
                    {
                        //  ChangeAnim("idle");
                        
                      //  ChangeAnim("rage");
                      //Debug.Log(Vector3.Distance(transform.position, player.transform.position) - rangeAttack +" da chay vao tan cong");
                     //   StopMove();
                     
                      
                        isAttacking = true;
                        if (UnityEngine.Random.value < 0.3)
                        {
                          timerAttack = 3f;
                            
                            Attack2();
                        }
                        else 
                        {
                            if(UnityEngine.Random.value < 0.3)
                            {
                                stateMachine.ChangeState(AttackState1);
                            }
                            else
                            {
                              //timerAttack = 1f;
                                Attack3();
                            }

                        }
                       
                        canAttack = false;
                    }
                    else if(canAttack == false)
                    {
                        StopMove();
                    }


                    if (timerAttack - couter < 0.01f)
                    {
                        
                        canAttack = true;
                        Debug.Log("reset canatk" + canAttack);
                        couter = 0;

                       // Debug.Log("attak");
                       // StopMove();
                       // isAttacking = true;

                       // Attack2();
                       //// canAttack = false;
                        // agent.isStopped = false;
                    }
                   

                }
                else if(isAttacking==false)
                {
                    Move(7f, player.transform.position);
                }



            }

        };

        onExit = () =>
        {
          //couter = 0;
            canAttack = true;
            isAttacking = false;
            StopMove();
        };
    }

    public void AttackState3(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            agent.isStopped = true;


            timerAttack = 2f;

        };

        onExecute = () =>
        {
            if (player.IsDead == false)
            {

                // Debug.Log(tarpos);

                if (Vector3.Distance(transform.position, player.transform.position) < rangeAttack)
                {
                    //  agent.isStopped = true;
                    //  Debug.Log("attackE");
                    // agent.isStopped = true;
                    if (canAttack)
                    {
                      //  OnAttack();
                        canAttack = false;
                    }
                    couter += Time.deltaTime;
                    if (timerAttack - couter < 0.01f)
                    {
                        canAttack = true;
                        couter = 0;
                        // agent.isStopped = false;
                    }

                }


            }

        };

        onExit = () =>
        {
            ChangeAnim(Constant.ANIM_IDLE);
        };
    }

    //public void PatrolState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    //{
    //    onEnter = () =>
    //    {
    //        agent.isStopped = false;
    //        timerPatrol = UnityEngine.Random.Range(1f, 2f);
    //        agent.SetDestination(transform.position + new Vector3(UnityEngine.Random.Range(-3, 3), 0, UnityEngine.Random.Range(-3, 3)));
    //        ChangeAnim(Constant.ANIM_RUN);

    //    };

    //    onExecute = () =>
    //    {
    //        if (player.IsDead == false)
    //        {

    //            couter += Time.deltaTime;
    //            if (-couter + timerPatrol < 0.01f)
    //            {
    //              //  stateMachine.ChangeState(AttackState);
    //                couter = 0;
    //            }
    //        }


    //    };

    //    onExit = () =>
    //    {
    //        couter = 0;
    //        agent.ResetPath();
    //    };
    //}


    private void LoadBullet()
    {
     bl = Instantiate(bulletE, pointOfBullet.position, Quaternion.Euler(pointOfBullet.rotation.eulerAngles));
    
    }
    private void FireBulet()
    {
        //  l.OnInit(player.transform.position);
        bl.OnInit(player.transform.position);
        isAttacking = false;
    }
    private void Attack1()
    {
        
        LoadAttack1();

       
        
        
        //Vector3 rotL = new Vector3(pointOfBullet.rotation.eulerAngles.x, pointOfBullet.rotation.eulerAngles.y - 45, pointOfBullet.rotation.eulerAngles.z);
        //Vector3 rotR = new Vector3(pointOfBullet.rotation.eulerAngles.x, pointOfBullet.rotation.eulerAngles.y + 45, pointOfBullet.rotation.eulerAngles.z);
        //BulletE blL = Instantiate(bulletE, pointOfBullet.position, Quaternion.Euler(rotL));
        //BulletE blR = Instantiate(bulletE, pointOfBullet.position, Quaternion.Euler(rotR));
        Invoke(nameof(OnAttack1), 1f);
        // ParticleSystem vfx = Instantiate(vfxE, RoomManager.Ins.player.transform.position + Vector3.forward, Quaternion.identity);


    }
    private void LoadAttack1()
    {
        isLoadingAttack = true;
        transform.LookAt(player.transform);
        pointOfBullet.LookAt(player.transform);

        ChangeAnim("rage");
        Invoke(nameof(LoadBullet), 0.7f);


    }

    private void OnAttack1()
    {
        ChangeAnim("hit");

        //  bl.OnInit();
        Invoke(nameof(FireBulet), 0.2f);
        isLoadingAttack = false;

       
        //  attackArea.gameObject.SetActive(false);
   
    }

    private void Attack2()
    {
       
        
        LoadAttack2();
        Vector3 tarPosAttack = player.transform.position;
        InswaringCircle.transform.position = tarPosAttack;
        InswaringCircle.SetActive(true);
        // ChangeAnim("jumpAttack");
        //  Invoke(nameof(OnAttack2(tarPosAttack), 1f);
        // ParticleSystem vfx = Instantiate(vfxE, RoomManager.Ins.player.transform.position + Vector3.forward, Quaternion.identity);

        StartCoroutine(OnAttack2Crt(tarPosAttack));
    }

    private void LoadAttack2()
    {
      //  Debug.Log(" trong ham load jump Att");

        ChangeAnim("jumpAttack");
       
       
        //agent.speed = 20f;
        //agent.SetDestination(player.transform.position);
        //  transform.position = Vector3.MoveTowards(transform.position, tarPosAttack, 1f);

        isLoadingAttack = true;
    
       

    }
    IEnumerator OnAttack2Crt(Vector3 tf)
    {
        
        //  Debug.Log("Coroutine started");

       //
        yield return new WaitForSeconds(1f); // Tạm dừng coroutine trong 2 giây
                                             //  Debug.Log("Coroutine resumed after 2 seconds");

        transform.position = tf;

        CameraShaker.Presets.Explosion3D();

        ChangeAnim("land");
        attackArea.gameObject.SetActive(true);
        isLoadingAttack = false;

        Invoke(nameof(ResetAttack2), 0.4f);

    }
    private void OnAttack2(Vector3 tf)
    {
        // ChangeAnim("hit");
        // Debug.Log("onAttak");
        transform.position = tf;
        attackArea.gameObject.SetActive(true);
        CameraShaker.Presets.Explosion3D();
        isLoadingAttack = false;
        
        Invoke(nameof(ResetAttack2), 0.2f);
        //  attackArea.gameObject.SetActive(false);
        
    }
    private void ResetAttack2()
    {
        attackArea.gameObject.SetActive(false);
        isAttacking = false;
        ChangeAnim(Constant.ANIM_IDLE);

        InswaringCircle.SetActive(false);
    }


    private void Attack3()
    {
        LoadAttack3();
        Invoke(nameof(OnAttack3), 0.3f);

    }
    private void LoadAttack3()
    {
        Vector3 looktf = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(looktf);
        ChangeAnim("hit2");
    }
    private void OnAttack3()
    {
        attackArea2.gameObject.SetActive(true);

        Invoke(nameof(ResetAttack3), 0.4f);
    }
    private void ResetAttack3()
    {
        attackArea2.gameObject.SetActive(false);
        isAttacking = false;
        ChangeAnim(Constant.ANIM_IDLE);

    }

    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        if (player.targetEnemy == this)
        {
           // stateMachine.ChangeState(AttackState);
        }

    }

    private void Move(float speed, Vector3 tarPos )
    {
      //  agent.ResetPath();
        agent.isStopped = false;

        agent.speed = speed;
        agent.SetDestination(tarPos);
        ChangeAnim(Constant.ANIM_RUN);
    }

    private void StopMove()
    {
        ChangeAnim("idle");
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        
        Debug.Log("stopMove");
        agent.isStopped = true;
       // agent.speed = 0;
       
    }

}


