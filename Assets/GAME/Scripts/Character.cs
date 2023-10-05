using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
  //      [SerializeField] protected GameObject parent;
    public float curHp;
    [SerializeField] public float maxHp;
   // [SerializeField] protected Slider hpBar;
    [SerializeField] protected HpBar HpBarPrefab;
    [SerializeField]    protected HpBar hpBar;

    [SerializeField] protected CombatText combatText;
    


    private string animName="";
    public Animator Anim;

    public bool IsDead => curHp <= 0;
    public bool IsDeaded=false;


    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            if (animName != "")
            {
                Anim.ResetTrigger(this.animName);
            }
           
            this.animName = animName;
            Anim.SetTrigger(this.animName);
        }
    }

    public virtual void OnInit()
    {
        IsDeaded = false;
         hpBar = Instantiate(HpBarPrefab, transform.position,HpBarPrefab.transform.rotation);
         hpBar.OnInit(this);
        

        ChangeAnim(Constant.ANIM_IDLE);

    }
    public virtual void OnDespawn()
    {
      
        Destroy(this.gameObject);
        
      //  Destroy(this.gameObject);
      //  ChangeAnim(Constant.ANIM_DIE);
    }
    public virtual void OnDeath()
    {

        Debug.Log("chaneAnimDieEnemy");
        ChangeAnim(Constant.ANIM_DIE);
        IsDeaded = true;
        Destroy(hpBar.gameObject);
       

        Invoke(nameof(OnDespawn),2f);
     //   Debug.Log("destroy");   
    }

    public virtual void OnHit(float damage)
    {
        if (!IsDead)
        {
            curHp -= damage;
            Instantiate(combatText, this.transform.position + Vector3.up, combatText.transform.rotation).OnInit(damage);
        }
         if(IsDead&& !IsDeaded)
        {
            Debug.Log("da chet");
            OnDeath();
            curHp = 0;
          
        }

        //healthBar.SetNewHp(hp);
      //????s


    }
}
