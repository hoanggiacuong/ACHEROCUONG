using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager :Singleton<ShopManager>
{
    public ShopDataCustom shopDataCustom;
    public List<ShopItemBase[]> ListShopItems = new List<ShopItemBase[]>();
  



    private void Start()
    {
        ListShopItems.Add(shopDataCustom.weaponsItems);
        ListShopItems.Add(shopDataCustom.hatItems);
        
        for(int i=0; i < ListShopItems.Count; i++)
        {
            for(int j=0; j < ListShopItems[i].Length; j++)
            {
                var item = ListShopItems[i][j];

                if (i==0 && j==0)
                {
                    PlayerData.SetBoolData(PlayerData.Key_Player_Weapon_Pefix + j, true); 
                }
                else
                {
                    if(!PlayerPrefs.HasKey(PlayerData.Key_Player_Weapon_Pefix + j))
                    {
                       PlayerData.SetBoolData(PlayerData.Key_Player_Weapon_Pefix + j, false);
                    }
                }


            }
        }
    }

}
