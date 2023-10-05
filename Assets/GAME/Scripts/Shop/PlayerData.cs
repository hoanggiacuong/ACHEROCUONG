using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData :ScriptableObject
{
    public const string Key_Cur_Player_Weapon = "Cur_PlayerWeapon";

    public const string Key_Player_Weapon_Pefix = "PlayerWeapon_";


    public const string Key_Cur_Player_Hat = "Cur_PlayerHat";

    public const string Key_Player_Hat_Pefix = "PlayerHat_";

    public const string Key_Coin = "Coins";

    
    public static int curWeaponID
    {
        set => PlayerPrefs.SetInt(Key_Cur_Player_Weapon, value);
        get => PlayerPrefs.GetInt(Key_Cur_Player_Weapon);
    }
    
    public static int curHatID
    {
        set => PlayerPrefs.SetInt(Key_Cur_Player_Hat, value);
        get => PlayerPrefs.GetInt(Key_Cur_Player_Hat);
    }
    public static int Coins
    {
        set => PlayerPrefs.SetInt(Key_Coin, value);
        get => PlayerPrefs.GetInt(Key_Coin);
    }

    public static void SetBoolData(string key, bool value)
    {
       
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static bool GetBoolData(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
}
