using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletBoss : BulletE
{
 // private Vector3 playerPos;
    // Start is called before the first frame update

    public  void OnInit(Vector3 playerPos)
    {
        // rb.DOJump(playerPos, 5f, 1,1);
        //transform.DOMove(playerPos, 2f, false);

        transform.DOJump(playerPos, 8f, 1, 2f).SetEase( Ease.Linear);
      //  rb = GetComponent<Rigidbody>();
        Invoke(nameof(OnDespaw), 3f);
      //this.playerPos = playerPos;


    }

    private void OnDespaw()
    {
        DOTween.Kill(transform);
        Destroy(this.gameObject);
    }


}
