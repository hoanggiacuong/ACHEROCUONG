using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
   [SerializeField] bool isPlayerInRoom = false;
    //List<Enemy> listE;
    public List<EnemyPointData> listDataE = new List<EnemyPointData>();
    public Player player;
    //public List<Transform> listTransformEnemyFly = new List<Transform>();
    //public List<Transform> listTransformEnemyClose = new List<Transform>();
    //public List<Transform> listTransformEnemyTank = new List<Transform>();


    public Transform pointStart;
    public Transform pointNextRoom;
    public GameObject door;


    // [SerializeField] public Dictionary<Enemy, List<Vector3>> enemyPositions = new Dictionary<Enemy, List<Vector3>>();
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log("vao phong");
        if (other.CompareTag(Constant.PLAYER))
        {
            //  Debug.Log("clear li");

            RoomManager.Ins.liEnemyInRoom.Clear();
            player = other.GetComponent<Player>();
            isPlayerInRoom = true;
        }

        //if (isPlayerInRoom)
        //{
        //    if (other.CompareTag(Constant.ENEMY))
        //    {
        //        // Debug.Log("add lis");
        //        RoomManager.Ins.liEnemyInRoom.Add(other.GetComponent<Enemy>());
        //    }
        //}

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER))
        {

            isPlayerInRoom = false;
        }
    }
}
