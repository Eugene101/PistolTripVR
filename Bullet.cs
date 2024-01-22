using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    bool isOnTarget;
    [SerializeField]float bulletSpeed;
    HealthController healthController;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.transform.parent = null;
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isOnTarget)
        {
            rb.AddForce(Vector3.forward * bulletSpeed);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name + "Vasya");
    }

}
