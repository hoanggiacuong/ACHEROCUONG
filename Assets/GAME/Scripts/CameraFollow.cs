using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 tarpos = new Vector3(0, target.position.y, target.position.z);
            transform.position = Vector3.Lerp(transform.position, tarpos + offset, Time.fixedDeltaTime * speed);
        }

    }
}
