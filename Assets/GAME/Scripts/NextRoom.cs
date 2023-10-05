using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER)&& RoomManager.Ins.liEnemyInRoom.Count==0)
        {
            RoomManager.Ins.NextRoom();
        }
    }
}
