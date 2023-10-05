using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Enemy : Character
{


    public override void OnInit()
    {
        base.OnInit();
       // ChangeAnim(Constant.ANIM_IDLE);
        maxHp = 100 * RoomManager.Ins.numOfRoom;
        curHp = maxHp;

    }



    // Start is called before the first frame update



}
