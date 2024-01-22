using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFLY : MonoBehaviour
{
    Rigidbody rb;
    bool isOnTarget;
    LineRenderer lineRenderer;
    public float tailLenght;
    public GameObject tail;
    GameObject bang;
    Vector3 shotDot;
    [SerializeField] float bulletSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bang = transform.GetChild(1).gameObject;
        print(bang.name + " cat");
        bang.SetActive(false);
        transform.parent = null;
        rb.velocity = Vector3.zero;
        //lineRenderer = GetComponent<LineRenderer>();
        //shotDot = GameObject.Find("BulletPoint");
        shotDot = transform.position;
        tail = transform.GetChild(0).gameObject;
        //slowPartner = Instantiate(slowPartner, transform.position, Quaternion.identity);
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnTarget)
        {
            //rb.AddForce(Vector3.forward * bulletSpeed);
            transform.Translate(Vector3.forward * bulletSpeed);
            //lineRenderer.SetPosition(0, shotDot);
            //lineRenderer.SetPosition(1, transform.position);
            //shotDot = (shotDot + transform.position) *tailLenght;
            Vector3 tmp = tail.transform.localScale;
            tmp.y += tailLenght;
            tail.transform.localScale = tmp;
            //s lowPartner.transform.Translate(Vector3.forward * bulletSpeed*0.5f);
        }

        //if (slowPartner!=null)
        //{

        //}


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Enemy"))
        {
            rb.velocity = Vector3.zero;
            var enemyScript = other.GetComponent<Enemy>();
            bang.SetActive(true);
            bang.transform.localScale *= 30;
            Destroy(gameObject, 2f);
            if (enemyScript != null) { enemyScript.EnemyDie(); }
            else
            {
                print("Error: can`t find EnemyScript");
            }
        }
    }
}
