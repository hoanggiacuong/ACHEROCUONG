using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] private List<Room> listRoomNormalPrefab = new List<Room>();

    //[SerializeField] private EnemyClose enemyCloseFrefab;
    //[SerializeField] private EnemyFly enemyFlyFrefab;
    //[SerializeField] private EnemyTank enemyTankFrefab;



    //[SerializeField] private List<Room> listRoomBosPrefab = new List<Room>();
    //[SerializeField] private List<Room> listRoomAwardPrefab = new List<Room>();



    public List<Enemy> liEnemyInRoom = new List<Enemy>();
    public Room curRoom;
    [SerializeField] public int numOfRoom;
    public Player player;// => curRoom.player;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (liEnemyInRoom.Count == 0&& curRoom!=null)
        {
            curRoom.door.SetActive(false);
        }
    }



    public void ClearEnemy()
    {

        if (liEnemyInRoom.Count > 0)
        {
            for (int i = 0; i < liEnemyInRoom.Count; i++)
            {
                liEnemyInRoom[i].OnDeath();
            }
        }

        liEnemyInRoom.Clear();

        //   Destroy(curRoom.gameObject, 0.2f);

    }

    private void OnInitEnemyRoom(Room room) 
    {
       
        for(int i=0; i<room.listDataE.Count; i++)
        {
            for(int j=0; j < room.listDataE[i].pointOfEnemies.Count; j++)
            {
                Enemy e = Instantiate(room.listDataE[i].enemyPrefab, room.listDataE[i].pointOfEnemies[j].position, Quaternion.identity);
                Debug.Log("load E");
                e.OnInit();
                liEnemyInRoom.Add(e);
               
            }
        }




        //for(int i=0; i< room.listTransformEnemyClose.Count; i++)
        //{
        //   Enemy e=  Instantiate(enemyCloseFrefab, room.listTransformEnemyClose[i].position, Quaternion.identity);
        //    e.OnInit();
        //    liEnemyInRoom.Add(e);
        //}
        //for (int i = 0; i < room.listTransformEnemyFly.Count; i++)
        //{
        //    Enemy e = Instantiate(enemyFlyFrefab, room.listTransformEnemyFly[i].position, Quaternion.identity);
        //    e.OnInit();
        //    liEnemyInRoom.Add(e);
        //}

        //for (int i = 0; i < room.listTransformEnemyTank.Count; i++)
        //{
        //    Enemy e = Instantiate(enemyTankFrefab, room.listTransformEnemyTank[i].position, Quaternion.identity);
        //    e.OnInit();
        //    liEnemyInRoom.Add(e);
        //}

    }






    //public void loadRoom()
    //{
    //    loadlevel(numOfRoom);
    //  //  oninit();
    //}
    public void loadRoom(int indexRoom)
    {
        if (curRoom!= null)
        {
            Destroy(curRoom.gameObject);
        }   

        curRoom = Instantiate(listRoomNormalPrefab[indexRoom-1]);

        OnInitEnemyRoom(curRoom);
        player.transform.position = curRoom.pointStart.transform.position;
    }
    public void NextRoom()
    {
        numOfRoom += 1;
        loadRoom(numOfRoom);
        
    }

    public void ReTry()
    {
        //for(int i=0;  i<liEnemyInRoom.Count; i++)
        //{
        //    liEnemyInRoom[i].OnDespawn();
        //}
        //liEnemyInRoom.Clear();
        loadRoom(numOfRoom);
        player.OnInitReTry();
        GameManager.Ins.ChangeState(GameState.GamePlay);
    }




    public void OnInit()
    {
        Debug.Log("init romm moi");
        numOfRoom = 1;
        //currentLevel.OnInit();
   
        //Debug.Log(hero.transform.position);

        loadRoom(1);
        if (numOfRoom == 1)
        {
            player.OnInit();
        }

       
       
    }

}
