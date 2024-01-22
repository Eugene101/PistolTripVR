using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisions : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Enemy"))
        {
            var enemyScript = other.GetComponent<Enemy>();
            if (enemyScript != null && enemyScript.enemyState!=Enemy.EnemyState.die)
            {
                enemyScript.EnemyDie();
            }
            else
            {
                print("Error: can`t find EnemyScript");
            }
        }
        else if (other.tag == "OppositeBullet")
        {
            Destroy(other);
        }
    }
}
