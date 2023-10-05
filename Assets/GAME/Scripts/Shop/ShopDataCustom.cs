using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ShopDataCustom")]
public class ShopDataCustom : ScriptableObject
{
 
    public ShopItemWeapon[] weaponsItems;
    public ShopItemHat[] hatItems;
      
}


[System.Serializable]
public class ShopItemBase
{
    public int price;
    public Sprite hud;
}
[System.Serializable]
public class ShopItemWeapon:ShopItemBase
{

    public WeaponType weaponType; 
}
[System.Serializable]
public class ShopItemHat:ShopItemBase
{

    public HatType hatType;
}