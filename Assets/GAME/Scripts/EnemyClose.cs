using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyClose : Minion
{
    [SerializeField] protected StateMachine stateMachine = new StateMachine();
    [SerializeField] protected float rangeAttack;
    [SerializeField] protected float rangeSingt=10f;
    [SerializeField] private float damage=10f;
    [SerializeField] private bool canAttack=true;
    [SerializeField] AttackArea attackArea;

    public ParticleSystem vfxE;


    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float moveSpeed = 8f;
    private float timerIdle ;
    private float timerPatrol;
    private float timerAttack;

    private  float couter = 0;

    private Player player => RoomManager.Ins.player;



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(IsDead);
        //if (hpBar != null)
        //{
        //    hpBar.transform.position = transform.position;
        //    hpBar.hpBarSlide.value = Mathf.Lerp(hpBar.hpBarSlide.value, curHp / maxHp, Time.deltaTime * 5f);
        //    hpBar.textHp.text = curHp.ToString();
        //}

        if (IsDead)
        {
            RoomManager.Ins.liEnemyInRoom.Remove(this);
            ChangeAnim(Constant.ANIM_DIE);

        }
        else
        {
           
            stateMachine?.Execute();
        }

        //    hpBar.value = Mathf.Lerp(hpBar.value, curHp / maxHp, Time.deltaTime * 5f);
        //  Debug.Log(hpBar.value);



         

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
        attackArea.damage = damage;
    }
    public void IdleState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            timerIdle = 2f;
           
            StopMove();
        };

        onExecute = () =>
        {
            couter += Time.deltaTime;
            if (-couter +timerIdle < 0.01f)
            {
                stateMachine.ChangeState(PatrolState);
                couter = 0;
            }
            
        };

        onExit = () =>
        {
           
        };
    }


    public  void AttackState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            canAttack = true;
            agent.speed = moveSpeed;
            agent.stoppingDistance =2f;
            rangeAttack = 3f;
            timerAttack = 1f;
            
        };

        onExecute = () =>
        {
            if (player.IsDead==false)
            {

               // Debug.Log(tarpos);

                if (Vector3.Distance(transform.position,player.transform.position) < rangeAttack)
                {
                   
                    //  agent.isStopped = true;
                  //  Debug.Log("attackE");
                    // agent.isStopped = true;
                    if (canAttack)
                    {
                        StopMove();
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
                    Vector3 tarpos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                    Move(tarpos);
                }


            }
            
        };

        onExit = () =>
        {
            ChangeAnim(Constant.ANIM_IDLE);
        };
    }

    public  void PatrolState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            timerPatrol = UnityEngine.Random.Range(2, 4);
            agent.SetDestination(transform.position + new Vector3(UnityEngine.Random.Range(-3,3 ),0, UnityEngine.Random.Range(-3,3)));
            ChangeAnim(Constant.ANIM_RUN);

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

    public void DieState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
          

            StopMove();
            ChangeAnim(Constant.ANIM_DIE);
        };

        onExecute = () =>
        {


        };

        onExit = () =>
        {

        };
    }

    private void OnAttack()
    {
        transform.LookAt(player.transform);
        ChangeAnim(Constant.ANIM_ATTACK);

        attackArea.gameObject.SetActive(true);
        Invoke(nameof(ResetAttack),0.4f);
        ParticleSystem vfx = Instantiate(vfxE, RoomManager.Ins.player.transform.position+Vector3.forward,Quaternion.identity);


    }

    private void ResetAttack()
    {
        attackArea.gameObject.SetActive(false);
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

    private void Move(Vector3 tarPos)
    {
        agent.isStopped = false;
        ChangeAnim(Constant.ANIM_RUN);
        agent.SetDestination(tarPos);
    }
    private void StopMove()
    {
        ChangeAnim(Constant.ANIM_IDLE);
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
       
        agent.ResetPath();
       

       


    }
    public override void OnDeath()
    {
        StopMove();
      //  stateMachine.ChangeState(null);
        base.OnDeath();

    }
    public override void OnDespawn()
    {
        base.OnDespawn();
      
    }


}
