using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : UICanvas
{
    public Text textCoins;
    
    public void PlayButton()
    {
        CloseDirectly();
        GameManager.Ins.StartGame();
        UIManager.Ins.OpenUI<GamePlay>();
       
    }

    public void ShopButton()
    {
         UIManager.Ins.OpenUI<ShopUICustom>();
      
    }

    public void UpDateUiCoin()
    {
        textCoins.text = PlayerData.Coins.ToString();
        
    }
    public override void Open()
    {
        base.Open();
        UpDateUiCoin();
    }

}
