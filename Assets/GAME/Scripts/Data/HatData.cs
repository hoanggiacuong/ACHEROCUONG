using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "HatData")]
public class HatData : ScriptableObject
{
  
   
    
        [SerializeField] GameObject[] Hat;

        public GameObject GetHat(HatType hat)
        {
            return  Hat[(int)hat];

        }
    

}
