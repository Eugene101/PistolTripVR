using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    float maxRaycastDistance = 100f;
    // Start is called before the first frame update
    void Update()
    {
        // Casts a ray from the position of this GameObject's transform in the forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        // Check if the ray hits something within a specified distance
        if (Physics.Raycast(ray, out hitInfo, maxRaycastDistance))
        {
            // Do something with the object hit
          
            if (hitInfo.collider.gameObject.tag.Contains("Enemy"))
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                if (enemy != null) {
                    print ("Vasya" +enemy.name);
                    print("Joj");
                }
            }
            // You can access other information like hitInfo.point, hitInfo.distance, etc.
        }
    }
}

