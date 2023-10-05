using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUICustom : UICanvas
{
    public Transform GridRoot;
    public ShopItemUI itemUiPrefab;

    public ShopDataCustom shopDataCustom;
    public List<ShopItemBase[]> ListShopItems = new List<ShopItemBase[]>();

    public Text textCoins;

    Player player => RoomManager.Ins.player;

    Button btn;
    

    private void Start()
    {

        ListShopItems.Add(shopDataCustom.weaponsItems);
        ListShopItems.Add(shopDataCustom.hatItems);

        for (int i = 0; i < ListShopItems.Count; i++)
        {
            for (int j = 0; j < ListShopItems[i].Length; j++)
            {
                var item = ListShopItems[i][j];

                if (i == 0 )
                {
                    if (j == 0)
                    {
                        PlayerData.SetBoolData(PlayerData.Key_Player_Weapon_Pefix + j, true);
                    }
                    else
                    {
                        if (!PlayerPrefs.HasKey(PlayerData.Key_Player_Weapon_Pefix + j))
                        {
                            PlayerData.SetBoolData(PlayerData.Key_Player_Weapon_Pefix + j, false);
                        }
                    }

                }
                if(i==1)
                {
                    if (j == 0)
                    {
                        PlayerData.SetBoolData(PlayerData.Key_Player_Hat_Pefix + j, true);
                    }
                    else
                    {
                        if (!PlayerPrefs.HasKey(PlayerData.Key_Player_Hat_Pefix + j))
                        {
                            PlayerData.SetBoolData(PlayerData.Key_Player_Hat_Pefix + j, false);
                        }
                    }
                }



            }
        }


        UpdateUI(shopDataCustom.weaponsItems);
        
    }

    public void ActiveBtn(Button btn)
    {
        btn.GetComponent<Outline>().enabled = true;
        btn.image.color = Color.green;
    }
    public void DeActiveBtn(Button btn)
    {
        btn.GetComponent<Outline>().enabled = false;
        btn.image.color = new Color(255f, 255f, 255f, 255f);
    }

    private void UpDateUiCoin()
    {
        textCoins.text = PlayerData.Coins.ToString();
    }


   public override void Open()
    {
        base.Open();


        UpdateUI(shopDataCustom.weaponsItems);

    }

    public void ShopWeaponBtn()
    {
        UpdateUI(shopDataCustom.weaponsItems);
    }

    public void ShopHatBtn()
    {
        UpdateUI(shopDataCustom.hatItems);
    }


    public void UpdateUI(ShopItemBase[] items)
    {
        ClearUI();
        UpDateUiCoin();
        //Debug.Log("updateui");
        if (items == null || items.Length <= 0 || !GridRoot || !itemUiPrefab) return;

        // ve ui
        for(int i =0; i < items.Length; i++)
        {
            // Debug.Log("updateui for");
            var item = items[i];
            var index = i;
            var itemUiClone = Instantiate(itemUiPrefab, GridRoot);

            itemUiClone.UpDateUI(item,index);
            if (itemUiClone.btn)
            {
                itemUiClone.btn.onClick.RemoveAllListeners();
                itemUiClone.btn.onClick.AddListener(() => ItemEvent(item,index));
                
            }
        }
    }

    void ItemEvent(ShopItemBase item, int shopItemid)
    {
        if (item == null) return;
        if (item is ShopItemWeapon)
        {
            ShopItemWeapon itemWP = item as ShopItemWeapon;
            bool IsUnLocked = PlayerData.GetBoolData(PlayerData.Key_Player_Weapon_Pefix + shopItemid);
            if (IsUnLocked)
            {
                Debug.Log(" item mo khoa");
                if (shopItemid == PlayerData.curWeaponID) return;
                PlayerData.curWeaponID = shopItemid;
                //  UpdateUI(shopDataCustom.weaponsItems);
                if (player)
                {
                    Debug.Log(" changewp da mo khoa");
                    player.ChangeWeapon(itemWP.weaponType);
                }
              
                


            }
            else
            {
                if (PlayerData.Coins >= item.price)
                {
                    PlayerData.Coins -= item.price;
                    PlayerData.SetBoolData(PlayerData.Key_Player_Weapon_Pefix + shopItemid, true);
                    PlayerData.curWeaponID = shopItemid;

                    if (player)
                    {
                        Debug.Log(" changewp chua va mua mo khoa");
                        player.ChangeWeapon(itemWP.weaponType);
                    }


                }
                else
                {
                    Debug.Log("NẠP TIỀN ĐI");
                }
            }
            UpdateUI(shopDataCustom.weaponsItems);
        }

        else if (item is ShopItemHat)
        {

            ShopItemHat itemHAT = item as ShopItemHat;
            bool IsUnLocked = PlayerData.GetBoolData(PlayerData.Key_Player_Hat_Pefix + shopItemid);
            if (IsUnLocked)
            {
                    Debug.Log(" item mo khoa");
                    if (shopItemid == PlayerData.curHatID) return;
                    PlayerData.curHatID = shopItemid;

                    if (player)
                    {
                        Debug.Log(" changehat da mo khoa");
                        player.ChangeHat(itemHAT.hatType);
                    }


              //  MiniPool


            }
            else
            {
                if (PlayerData.Coins >= item.price)
                {
                    PlayerData.Coins -= item.price;
                    PlayerData.SetBoolData(PlayerData.Key_Player_Hat_Pefix + shopItemid, true);
                    PlayerData.curHatID = shopItemid;


                    if (player)
                    {
                        Debug.Log(" changehat chua va mua mo khoa");
                        player.ChangeHat(itemHAT.hatType);
                    }


                }
                else
                {
                    Debug.Log("NẠP TIỀN ĐI");
                }
            }
            UpdateUI(shopDataCustom.hatItems);
        }
    }
    public void ClearUI()
    {
        //dùng pool toi uu
        if (!GridRoot || GridRoot.childCount <= 0) return;
        for (int i = 0; i < GridRoot.childCount; i++)
        {
            var item = GridRoot.GetChild(i);
            if (item)
            {
                Destroy(item.gameObject);
            }
           
        }
    }

}

