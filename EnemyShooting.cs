using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    GameObject car;
    Transform shotPoint;
    private LineRenderer lineRenderer;
    public GameObject enemyBullet;
    public GameObject enemyScopePrefab;
    [SerializeField] int numberOfshoots = 1;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.Find("Car");
        shotPoint = transform.GetChild(0).GetChild(1).GetChild(2).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        lineRenderer = GetComponent<LineRenderer>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemy.iCanShot)
        //{
        //    lineRenderer.SetPosition(0, shotPoint.position);
        //    lineRenderer.SetPosition(1, car.transform.position);
        //}
        if (enemy.iCanShot && numberOfshoots>1)
        {
            GameObject enemyBulletShot = Instantiate(enemyBullet, transform.position, transform.rotation);
            GameObject enemyScope = Instantiate(enemyScopePrefab, transform.position, transform.rotation);
            enemyScope.transform.parent = shotPoint;
            enemyScope.transform.LookAt(car.transform.position);
            //Destroy(enemyScope, 3f);
            numberOfshoots--;
        }
    }
}
