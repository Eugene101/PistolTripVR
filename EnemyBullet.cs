using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    GameObject car;
    [SerializeField] float enemyBulletSpeed;
    [SerializeField] float damagetime;
    GameObject carTarget;
    GameObject carLifebar;
    bool ifIGet;
    Vector3 shift;
    LifeBar lifebarScript;
    void Start()
    {

        car = GameObject.Find("Car");
        carTarget = car.transform.GetChild(0).gameObject;
        carLifebar = car.transform.GetChild(0).GetChild(0).gameObject;
        lifebarScript = carLifebar.GetComponent<LifeBar>();
        transform.LookAt(carTarget.transform.position);
        float Shiftx = Random.Range(-5f, 5f);
        float Shifty = Random.Range(-1f, 0f);
        float Shiftz = Random.Range(-5f, 5f);

        if (Shiftx <= 3 && Shiftz <= 3 && Shiftx >= -3 && Shiftz >= -3)
        {
            Invoke("MakeDamage", damagetime);
        }

        shift = new Vector3(Shiftx, Shifty, Shiftz);
        Destroy(gameObject, 1.4f);
    }

    void MakeDamage()
    {
        //Vector3 decrVector = new Vector3(0f, 0f, 0.3f);
        //carTarget.transform.localScale -= decrVector;
        lifebarScript.MinusLife();
    }
    //void CheckTarget(float Shiftx, float Shiftz)
    //{
    //    if (Shiftx <= 2 && Shiftz<=2 && Shiftx >=-2 && Shiftz >= -2)
    //    {
    //        print("Shooted");
    //        Vector3 decrVector = new Vector3(0f, 0f, 0.1f);
    //        carTarget.transform.localScale -= decrVector;
    //    }

    //    else
    //    {
    //        print("Missed");
    //    }
    //}    

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.name == "Car")
    //    {
    //        print("Shooted");
    //        Destroy(gameObject);
    //        Vector3 decrVector = new Vector3(0f, 0f, 0.1f);
    //        carTarget.transform.localScale -= decrVector;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "Car")
    //    {
    //        print("Shooted");
    //        Destroy(gameObject);
    //        Vector3 decrVector = new Vector3(0f, 0f, 0.3f);
    //        carTarget.transform.localScale -= decrVector;
    //    }

    //    else if (other.gameObject.name == "carRendered")
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        Vector3 target = carTarget.transform.position + shift;
        var dist = Vector3.Distance(transform.position, target);
        transform.position = Vector3.Lerp(transform.position, target, enemyBulletSpeed);

        if (dist < 0.4f)
        {
            Destroy(gameObject);
        }

    }
}
