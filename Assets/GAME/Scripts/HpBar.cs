using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HpBar : MonoBehaviour
{

    [SerializeField] public Slider hpBarSlide;
    [SerializeField] public TMP_Text textHp;
    Character character;


    public void OnInit(Character character)
    {
        this.character = character;
    }

    
    void Update()
    {
        if (character != null)
        {
            hpBarSlide.value = Mathf.Lerp(hpBarSlide.value,character.curHp /character.maxHp, Time.deltaTime * 5f);
            transform.position = character.transform.position;
            if (textHp != null)
            {
                textHp.text = character.curHp.ToString();
            }

        }

    }
}
