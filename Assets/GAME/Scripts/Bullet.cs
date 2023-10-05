using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField]private float moveSpeed = 30f;
    [SerializeField] private float damage = 10f;
    Rigidbody rb;
    public ParticleSystem vfx1;

    Vector3 direct;
    
    // Start is called before the first frame update


    public void OnInit(Vector3 direct)
    {
        rb = GetComponent<Rigidbody>();
        this.direct = direct;
        rb.velocity = direct * moveSpeed;
    }
    //private void Update()
    //{
    //    // transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    //    // rb.velocity = -transform.up * moveSpeed ;
    //    if (rb)
    //    {
            
    //    }
       

    //}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.ENEMY))
        {
            // Debug.Log("huy");

            ParticleSystem vfx = Instantiate(vfx1, transform.position, Quaternion.identity);
            SoudManager.Ins.PlaySfxSound("WeaponPlayerHit", 1f);
            //      Destroy(vfx, 0.1f);
          //  Destroy(this.gameObject);
            SimplePool.Despawn(this);
            other.gameObject.GetComponent<Enemy>().OnHit(damage);



        }

    }

    private void OnCollisionEnter(Collision collision)
    {



        if (collision.gameObject.CompareTag(Constant.WALL))
        {
            SoudManager.Ins.PlaySfxSound("WallHit", 1f);
            rb.velocity = Vector3.zero;
            SimplePool.Despawn(this);
           // Destroy(this.gameObject,2f);
        }
    }


}
