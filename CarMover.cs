using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;
using UnityEngine.AI;

public class CarMover : MonoBehaviour
{
    [SerializeField] float carSpeed;
    RoadManager roadManager;
    [SerializeField] float startingDelay;
    public enum WhatNext { curve, straight };
    int pointNumber = 0;
    bool runToNextPoint;
    Vector3 newPosition;
    public GameObject targetSpehre;
    ObjectsManager objectsManager;
    EnemyManager enemyManager;
    GameManager gameManager;
    private NavMeshAgent navMeshAgent;
    //public Transform targetPoint;
    Vector3 shift;
    int clearNumber = 0;
    int activeNumber = 0;
    int enemyNumber = 0;
    public WhatNext whatNext;

    [SerializeField] GameObject startSign;

    public static bool isLeftGunGrabbed;
    public static bool isRightGunGrabbed;
    public static bool iCanGo;

    // Start is called before the first frame update
    void Start()
    {
        objectsManager = GameObject.Find("ObjectsManager").GetComponent<ObjectsManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        roadManager = GameObject.Find("RoadManager").GetComponent<RoadManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        startSign.gameObject.SetActive(true);
    }

    public void Race()
    {

        shift = new Vector3(0, gameObject.transform.position.y, 0);
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (roadManager.targets[0] != null)
        {
            transform.position = roadManager.pieces[0].transform.GetChild(0).GetChild(0).transform.position/* + shift*/;
            //transform.LookAt(roadManager.pieces[1].transform.position + shift);
            //Invoke("GoRace", startingDelay);
        }
    }

    private void Update()
    {
        if (Menu.isTwoGuns)
        {
            if (!isLeftGunGrabbed && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Left, UxrInputButtons.Grip))
            {
                isLeftGunGrabbed = true;
            }
        }

        else
        {
            isLeftGunGrabbed = true;
        }

        if (!isRightGunGrabbed && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Grip))
        {
            isRightGunGrabbed = true;
        }

        if (isRightGunGrabbed && isLeftGunGrabbed && !iCanGo)
        {
            iCanGo = true;
            Invoke("GoRace", startingDelay);
        }



    }

    void GoRace()
    {
        pointNumber++;
        startSign.gameObject.SetActive(false);
        if (pointNumber < roadManager.piecesNumber)
        {
            Transform centrePoint = roadManager.targets[pointNumber];
            if (centrePoint.name == "1")
            {
                clearNumber++;


                if (clearNumber > 2)
                {
                    objectsManager.ClearOld();
                    activeNumber = 2;
                }

                else
                {
                    activeNumber++;
                    enemyNumber++;
                }

                //objectsManager.ShowRoads(activeNumber);
                enemyManager.ActivePiece(enemyNumber);

                //clearnumber = 0;
            }
            newPosition = new Vector3(centrePoint.position.x, centrePoint.position.y, centrePoint.position.z) + shift;
            //transform.LookAt(newPosition);
            runToNextPoint = true;
            RunAgent(newPosition);
        }

        else
        {
            gameManager.LevelEnd();
        }

    }


    void RunAgent(Vector3 newPosition)
    {
        targetSpehre.transform.position = newPosition;
        navMeshAgent.SetDestination(newPosition);
    }


    void FixedUpdate()
    {
        var dist = Vector3.Distance(transform.position, newPosition);
        if (runToNextPoint)
        {
            //transform.position = Vector3.Lerp(transform.position, newPosition, carSpeed);
            if (dist < 0.5f)
            {
                runToNextPoint = false;
                startSign.gameObject.SetActive(false);
                GoRace();
            }
        }
    }
}
