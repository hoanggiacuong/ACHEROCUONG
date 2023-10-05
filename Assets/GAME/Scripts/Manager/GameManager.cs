using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay, Finish, Setting }

public class GameManager : Singleton<GameManager>
{
    private GameState gameState;

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState) => this.gameState == gameState;

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        //Init data
       // UserData.Ins.OnInitData();
    }
    void Start()
    {
        // OnInit();
        ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();

    }

    public void StartGame()
    {
        //  PlayerData.curWeaponID = 0;
        //  PlayerData.curHatID = 0;
        // if(PlayerPrefs.HasKey(Coins))
        SoudManager.Ins.PlayThemSound("themeSound1");
        PlayerData.Coins = 10000;

        Debug.Log(PlayerData.Coins);
        ChangeState(GameState.GamePlay);
        RoomManager.Ins.OnInit();
    }

    public void EndGame()
    {
        SoudManager.Ins.themeSource.Stop();
        //   Destroy(curRoom.gameObject, 0.2f);
        ChangeState(GameState.Finish);
        RoomManager.Ins.ClearEnemy();
        UIManager.Ins.OpenUI<Fail>();
       

    }
}
