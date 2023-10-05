using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : GameUnit
{
    public Text priceText;
    public Image hud;
    public Button btn;

    public void UpDateUI(ShopItemBase item, int shopItemid)
    {
        if (item == null) return;
        if (hud)
        {
            hud.sprite = item.hud;
        }



        if (item is ShopItemWeapon)
        {
            bool IsUnLocked = PlayerData.GetBoolData(PlayerData.Key_Player_Weapon_Pefix + shopItemid);
            if (IsUnLocked)
            {
                if(shopItemid==PlayerData.curWeaponID)
                {
                    priceText.text = "EQUIED";
                    btn.image.color = Color.green;
                }

                else
                {
                    priceText.text = "CHOSE";
                }
            }
            else
            {
                priceText.text = item.price.ToString();
            }
        }

        else if(item is ShopItemHat)
        {
            bool IsUnLocked = PlayerData.GetBoolData(PlayerData.Key_Player_Hat_Pefix + shopItemid);
            if (IsUnLocked)
            {
                if (shopItemid == PlayerData.curHatID)
                {
                    priceText.text = "EQUIED";
                    btn.image.color = Color.green;
                }

                else
                {
                    priceText.text = "CHOSE";
                }
            }
            else
            {
                priceText.text = item.price.ToString();
            }
        }


       
    }

}
