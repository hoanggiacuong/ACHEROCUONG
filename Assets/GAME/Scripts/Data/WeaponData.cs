using System.Collections;
using System.Collections.Generic;
using UnityEngine;






    [CreateAssetMenu(menuName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] Weapon[] weapon;

        public Weapon GetWeapon(WeaponType wp)
        {
            return weapon[(int)wp];

        }
    }


