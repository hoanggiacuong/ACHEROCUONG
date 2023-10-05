using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyFly : Minion
{
    [SerializeField] protected StateMachine stateMachine = new StateMachine();
    [SerializeField] protected float rangeAttack;
    [SerializeField] protected float rangeSingt = 30f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private Transform pointOfBullet;
    [SerializeField] private Transform Skin;

    [SerializeField] private BulletE bulletE;

   // public ParticleSystem vfxE;



    [SerializeField] private float moveSpeed = 0.01f;
    private float timerIdle;
    private float timerPatrol;
    private float timerAttack;

    private float couter = 0;

    private Player player => RoomManager.Ins.player;



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
            // OnDeath();
            ChangeAnim(Constant.ANIM_DIE);
            RoomManager.Ins.liEnemyInRoom.Remove(this);
        }
        else
        {
            stateMachine?.Execute();
        }

        //if (hpBar != null)
        //{
        //    hpBar.transform.position = transform.position;
        //    hpBar.hpBarSlide.value = Mathf.Lerp(hpBar.hpBarSlide.value, curHp / maxHp, Time.deltaTime * 5f);
        //}






    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, rangeSingt);
    //    Gizmos.DrawWireSphere(transform.position, rangeAttack);
    //    //  Gizmos.color = Color.red;

    //}
    public override void OnInit()
    {
        base.OnInit();
        stateMachine.ChangeState(IdleState);

    }
    public void IdleState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            timerIdle = UnityEngine.Random.Range(1,2.5f);
            ChangeAnim(Constant.ANIM_IDLE);
        };

        onExecute = () =>
        {
            couter += Time.deltaTime;
            if (-couter + timerIdle < 0.01f)
            {
                stateMachine.ChangeState(PatrolState);
                couter = 0;
            }

        };

        onExit = () =>
        {

        };
    }


    public void AttackState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {

            // rangeAttack = 2f;
            ChangeAnim(Constant.ANIM_IDLE);
            timerAttack = 1f;
            

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
                        OnAttack();
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
                else
                {
                    ChangeAnim(Constant.ANIM_FLY);
                    transform.LookAt(player.transform);
                    //khoang cach la 4 theo OXZ
                    Vector3 tarpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, tarpos, Time.deltaTime * moveSpeed);
                }


            }

        };

        onExit = () =>
        {
            
        };
    }

    public void PatrolState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            timerPatrol = UnityEngine.Random.Range(2, 4);
          //  agent.SetDestination(transform.position + new Vector3(UnityEngine.Random.Range(-3, 3), 0, UnityEngine.Random.Range(-3, 3)));
           // ChangeAnim(Constant.ANIM_FLY);

        };

        onExecute = () =>
        {
            if (player.IsDead == false)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < rangeSingt)
                {
                    stateMachine.ChangeState(AttackState);

                }
                couter += Time.deltaTime;
                if (-couter + timerPatrol < 0.01f)
                {
                    stateMachine.ChangeState(IdleState);
                    couter = 0;
                }
            }


        };

        onExit = () =>
        {
            couter = 0;
        };
    }

    private void OnAttack()
    {
        ChangeAnim(Constant.ANIM_ATTACK);

        pointOfBullet.LookAt(player.transform);
        BulletE bl = Instantiate(bulletE, pointOfBullet.position,Quaternion.Euler(pointOfBullet.transform.rotation.eulerAngles));
        bl.OnInit();
        Invoke(nameof(ResetAttack), 0.2f);
     // ParticleSystem vfx = Instantiate(vfxE, RoomManager.Ins.player.transform.position + Vector3.forward, Quaternion.identity);


    }

    private void ResetAttack()
    {
     
        ChangeAnim(Constant.ANIM_IDLE);
    }

    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        if (player.targetEnemy == this)
        {
            stateMachine.ChangeState(AttackState);
        }

    }

}
