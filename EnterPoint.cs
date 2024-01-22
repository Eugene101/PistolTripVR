using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPoint : MonoBehaviour
{
    RoadManager roadManager;
    CarMover carMover;
    NavManager navManager;
    StuffManager stuffManager;
    ObjectsManager objectsManager;
    EnemyManager enemyManager;
    // Start is called before the first frame update
    private void Awake()
    {
        roadManager = GameObject.Find("RoadManager").GetComponent<RoadManager>();
        carMover = GameObject.Find("Car").GetComponent<CarMover>();
        navManager = GameObject.Find("NavigationManager").GetComponent<NavManager>();
        stuffManager = GameObject.Find("StuffManager").GetComponent<StuffManager>();
        objectsManager = GameObject.Find("ObjectsManager").GetComponent<ObjectsManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }

    /// <summary>
    /// set to public objects
    /// </summary>

    void Start()
    {
        roadManager.SetFirst();
        stuffManager.LoadStripsOff();
        navManager.SetNavigation();
        stuffManager.AmbientOn();
        stuffManager.LoadStripsOn();
        stuffManager.InstallBuildings();
        enemyManager.RotateEnemies();
        roadManager.SetFinish();
        carMover.Race();
        
        //objectsManager.HideRoads();
    }
}
