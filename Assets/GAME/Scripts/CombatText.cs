using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text comBatText;

    public void OnInit(float hp)
    {
        comBatText.text = hp.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

}
