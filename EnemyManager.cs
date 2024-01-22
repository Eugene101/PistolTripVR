using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    CarMover carMover;
    RoadManager roadManager;
    public GameObject[] enemies;
    public Transform[] carEnemiesDots;
    [SerializeField] int maxEnemies;
    public List<Transform> enemyPos = new List<Transform>();
    //float randCarEnemyInterval;
    //List<GameObject> enemiesList = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        carMover = GameObject.Find("Car").GetComponent<CarMover>();
        roadManager = GameObject.Find("RoadManager").GetComponent<RoadManager>();
    }

    private void Start()
    {
        InvokeRepeating("InstallCarEnemies", 5f, 4f);
    }

    void InstallCarEnemies()
    {
        if (!GameManager.isLevelEnd)
        {

            int randCarEnemyInterval = Random.Range(0, 5);
            int numbOfSoldiers = Random.Range(0, 5);

            if (randCarEnemyInterval > 1)
            {
                for (int i = 0; i < numbOfSoldiers; i++)
                {
                    int randEnemyCar = Random.Range(0, enemies.Length);
                    float shiftx = Random.Range(-3f, 3f);
                    float shiftz = Random.Range(-3f, 3f);
                    Vector3 shift = new Vector3(shiftx, 0, shiftz);
                    GameObject carEnemy = Instantiate(enemies[randEnemyCar], carEnemiesDots[i].position + shift, transform.rotation);
                    carEnemy.tag = "EnemyCar";
                }
            }
        }
    }

    public void ActivePiece(int enemyNumber)
    {
        enemyPos.Clear();

        //if (pointNumber>3)
        //{
        //    decrNumber = 1;
        //}


        GameObject activePiece = roadManager.pieces[enemyNumber];

        if (activePiece != null)
        {

            foreach (Transform item in activePiece.transform.GetChild(1).GetChild(0).GetChild(0))
            {

                if (item.tag == "EnemyDot")

                {
                    item.transform.rotation = transform.rotation * Quaternion.Euler(0, -180, 0);
                    enemyPos.Add(item);
                }
            }

            foreach (Transform item in activePiece.transform.GetChild(1).GetChild(1).GetChild(0))
            {
                if (item.tag == "EnemyDot")

                {
                    item.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 0);
                    enemyPos.Add(item);
                }
            }
        }
        InstallEnemy();
    }

    public void RotateEnemies()
    {

        for (int i = 0; i < roadManager.rotatedParts.Count; i++)
        {
            Transform rd0 = roadManager.rotatedParts[i].transform.GetChild(1).GetChild(0);
            rd0.name = "CityDot0rot";
            Transform rd1 = roadManager.rotatedParts[i].transform.GetChild(1).GetChild(1);
            rd1.name = "CityDot1rot";
            //foreach (Transform item in rd0)
            //{
            //    if (item.transform.name.Contains("Enemy"))
            //    {
            //        item.transform.rotation = transform.rotation * Quaternion.Euler(0, 180f, 0); 
            //    }
            //}
            //foreach (Transform item in rd1)
            //{
            //    if (item.transform.name.Contains("Enemy"))
            //    {
            //        item.transform.rotation = transform.rotation * Quaternion.Euler(0, -180f, 0);
            //    }
            //}


        }
    }

    private void InstallEnemy()
    {
        if (!GameManager.isLevelEnd)
        {
            for (int i = 0; i < enemyPos.Count; i++)
            {
                int randEnemy = Random.Range(0, enemies.Length);

                GameObject enemy = Instantiate(enemies[randEnemy], enemyPos[i].position, enemyPos[i].transform.rotation);
                enemy.transform.parent = enemyPos[i].transform;
                enemy.tag = "EnemyBuilding";
            }
        }
    }
}
