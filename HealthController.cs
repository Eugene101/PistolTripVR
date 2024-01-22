using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    int hp;
    DamageManager damageManager;
    void Start()
    {
        hp = GetComponent<Enemy>().health;
    }

    // Update is called once per frame
    
}
