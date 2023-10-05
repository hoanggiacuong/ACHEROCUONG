using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER))
        {
            other.GetComponent<Player>().OnHit(damage);
        }
    }
}
