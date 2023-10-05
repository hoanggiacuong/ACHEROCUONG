using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BulletE : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField]public float damage = 10f;
    protected Rigidbody rb;
    public ParticleSystem vfxE; 

    // Start is called before the first frame update
    //void Start()
    //{

    //}
    //private void Update()
    //{
    //    // transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
       
    //    //   rb.velocity = transform.forward  * moveSpeed;


    //}

    public virtual void OnInit()

    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * moveSpeed;
        Invoke("DestroyDOTween", 5f);
        Destroy(this.gameObject, 5f);
     

    }

    private void    OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag(Constant.PLAYER))
        {

            SoudManager.Ins.PlaySfxSound("RockHit", 1f);
            other.GetComponent<Player>().OnHit(damage);
            Instantiate(vfxE, transform.position - Vector3.forward * 0.3f, Quaternion.identity);
            Destroy(this.gameObject);
            DestroyDOTween();
          
        }
        
    }

    private void DestroyDOTween()
    {
        DOTween.Kill(transform);
    }
}
