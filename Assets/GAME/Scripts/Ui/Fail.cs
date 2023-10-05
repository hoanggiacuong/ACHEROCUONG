using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fail : UICanvas
{
    
    public void MainMenuButton()
    {
        Debug.Log("1");
        UIManager.Ins.OpenUI<MainMenu>();
        UIManager.Ins.CloseUI<GamePlay>();

        CloseDirectly();
        
    }

    public void RetryButton()
    {
        Debug.Log("2");
        RoomManager.Ins.ReTry();
        CloseDirectly();
    }
}
