using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    // Start is called before the first frame update
    //public  override  void OnInit()
    //{
    //    Debug.Log("init enemy nhen");
    //    base.OnInit();
    //    ChangeAnim(Constant.ANIM_IDLE);
    //    maxHp = 100 * RoomManager.Ins.numOfRoom;
    //    curHp = maxHp;

    //}
    //public override void OnHit(float damage)
    //{
    //    if (!IsDead)
    //    {
    //        curHp -= damage;
    //        Instantiate(combatText, this.transform.position + Vector3.up, combatText.transform.rotation).OnInit(damage);
    //    }
    //    if (IsDead && !IsDeaded)
    //    {
    //        Debug.Log("da chet");
    //        OnDeath();
    //        curHp = 0;

    //    }

    //    //healthBar.SetNewHp(hp);
    //    //????s


    //}

}
