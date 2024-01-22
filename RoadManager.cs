using System.Collections;
using System.Collections.Generic;
//using Unity.Burst.CompilerServices;
using UnityEngine;
using System.Linq;
//using Unity.XR.CoreUtils;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadParts;
    public int piecesNumber;
    public Quaternion[] rot;
    Quaternion currentRot;
    Quaternion straightRot;
    Quaternion curveRot;
    Vector3 pos;
    Vector3 shift;
    Vector3 enterPointShift;
    GameObject lastPlaced;
    //public GameObject firstRoad;
    public List<GameObject> pieces = new List<GameObject>();
    List<int> TurnSelectors = new List<int>();
    public List<Transform> targets = new List<Transform>();
    public List<Transform> currentTargets = new List<Transform>();
    public List<Transform> currentRotated = new List<Transform>();
    public List<GameObject> rotatedParts = new List<GameObject>();
    [SerializeField] GameObject finishPoint;
    enum WhatLast { turn0, turn1, turn2, turn3, curveRight, curveLeft, straight };
    WhatLast whatLast;
    bool isRotated;
    int shiftSide;
    int curveNum;
    public GameObject car;
    CarMover carMover;
    NavManager navManager;

    void Start()
    {
        //carMover = car.GetComponent<CarMover>();

        //SetFirst();

    }

    public void SetFirst()
    {
        TurnSelectors.Add(0); TurnSelectors.Add(2);
        curveNum = Random.Range(0, 2);
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = Vector3.zero;
            shift = new Vector3(0, 0, 16 * 4 * i);
            GameObject firstPiece = Instantiate(roadParts[0], pos + shift, transform.rotation);
            pieces.Add(firstPiece);
            targets.Add(firstPiece.transform.GetChild(0).GetChild(0).transform);
            whatLast = WhatLast.straight;
        }

        for (int j = 0; j < piecesNumber - 3; j++)
        {
            Selector();
        }

        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Centre");
       
        
        ////foreach (GameObject taggedObject in taggedObjects)
        ////{
        ////    targets.Add(taggedObject.transform);
        ////}


    }

    void Selector()
    {
        int rand = Random.Range(0, 13);
        pos = pieces.Last().transform.position;
        lastPlaced = pieces.Last();

        if (whatLast == WhatLast.straight)
        {
            if (rand < 5)
            {
                CreateStraightRoad();
            }

            else if (rand > 5 && rand < 10 && !isRotated)
            {
                CreateCurve();
            }

            else if (rand > 5 && rand < 10 && isRotated)
            {
                CreateStraightRoad();
            }

            else
            {
                CreateTurns();
            }
        }
        else
        {
            CreateStraightRoad();
        }


    }

    void CreateStraightRoad()
    {
        switch (whatLast)
        {
            case WhatLast.turn0:
                shift = new Vector3(64, 0, 16);
                straightRot = Quaternion.Euler(0, 90, 0);
                break;
            case WhatLast.turn1:
                if (isRotated)
                {
                    shift = new Vector3(-16, 0, 20);
                    straightRot = Quaternion.Euler(0, 90, 0);
                }
                else if (!isRotated)
                {
                    straightRot = Quaternion.Euler(0, 0, 0);
                    shift = new Vector3(8, 0, 64);
                }
                break;
            case WhatLast.turn2:

                shift = new Vector3(-64, 0, 16);
                straightRot = Quaternion.Euler(0, 90, 0);
                break;
            case WhatLast.turn3:
                if (!isRotated)
                {
                    straightRot = Quaternion.Euler(0, 0, 0);
                    shift = new Vector3(-16, 0, 64);
                }

                else if (isRotated)
                {
                    straightRot = Quaternion.Euler(0, 90, 0);
                    shift = new Vector3(8, 0, -8);
                }

                break;
            case WhatLast.curveRight:
                break;
            case WhatLast.curveLeft:
                if (!isRotated)
                {
                    shift = new Vector3(-24, 0, 64);
                    straightRot = Quaternion.Euler(0, 0, 0);
                }
                else if (isRotated)
                {
                    straightRot = Quaternion.Euler(0, 90, 0);
                    shift = new Vector3(64, 0, 24);
                }
                break;
            case WhatLast.straight:
                if (!isRotated)
                {
                    shift = new Vector3(0, 0, 64);
                    straightRot = Quaternion.Euler(0, 0, 0);
                }

                else
                {
                    shift = new Vector3(shiftSide * 64, 0, 0);
                    straightRot = Quaternion.Euler(0, 90, 0);
                }

                break;
        }

        GameObject newPiece = Instantiate(roadParts[0], pos + shift, transform.rotation * straightRot);

        if (straightRot == Quaternion.Euler(0, 90, 0))
        {
            rotatedParts.Add(newPiece);
        }

        pieces.Add(newPiece);
        targets.Add(newPiece.transform.GetChild(0).GetChild(0).transform);
        whatLast = WhatLast.straight;
    }

    void CreateTurns()
    {
        int randCorner = Random.Range(0, TurnSelectors.Count);
        int cornerNumber = TurnSelectors[randCorner];
        TurnSelectors.Clear();

        currentRot = rot[cornerNumber];
        switch (cornerNumber)
        {
            case 0:
                shift = new Vector3(16f, 0f, 64f);
                TurnSelectors.Add(1); curveNum = 1;
                whatLast = WhatLast.turn0;
                shiftSide = 1;
                break;
            case 1:
                shift = new Vector3(64f, 0f, 8f);
                TurnSelectors.Add(2); TurnSelectors.Add(0); curveNum = 0;
                whatLast = WhatLast.turn1;
                shiftSide = 1;
                break;
            case 2:
                shift = new Vector3(-8f, 0f, 64f);
                TurnSelectors.Add(3); curveNum = 1;
                whatLast = WhatLast.turn2;

                shiftSide = -1;
                break;
            case 3:
                if (isRotated)
                {
                    shift = new Vector3(-64, 0, 8);
                }
                TurnSelectors.Add(0); TurnSelectors.Add(2); curveNum = 1;
                whatLast = WhatLast.turn3;
                shiftSide = 1;
                break;
        }
        GameObject newCorner = Instantiate(roadParts[1], pos + shift, currentRot);
        pieces.Add(newCorner);
        //targets.Add(newCorner.transform.GetChild(0).GetChild(0));
        AddCornerTarget(newCorner);
        isRotated = !isRotated;

    }

    void AddCornerTarget(GameObject newCorner)
    {
        currentTargets.Clear();
        currentRotated.Clear();

        foreach (Transform points in newCorner.transform.GetChild(0))
        {

            if (points.tag == "Centre")
            {
                currentTargets.Add(points);
                currentRotated = currentTargets.OrderBy(go => go.name).ToList();

            }
        }

        if (whatLast == WhatLast.turn0 || whatLast == WhatLast.turn3)
        {
            currentRotated = currentTargets.OrderBy(go => go.name).ToList();
            foreach (var item in currentRotated)
            {
                targets.Add(item);
            }
        }

        else if (whatLast == WhatLast.turn1 || whatLast == WhatLast.turn2)
        {
            currentRotated = currentTargets.OrderBy(go => go.name).ToList();
            currentRotated.Reverse();


            foreach (var item in currentRotated)
            {
                targets.Add(item);
            }
        }
    }

    //


    void CreateCurve()
    {
        if (curveNum == 0)
        {
            whatLast = WhatLast.curveLeft;
            //if (isRotated)
            //{
            //curveRot = Quaternion.Euler(0, 90, 0);
            //shift = new Vector3(40, 0, 8);
            //}

            //else
            //{
            curveRot = Quaternion.Euler(0, 0, 0);
            shift = new Vector3(-8, 0, 64);
            //}
            ////targets.Add(newCurveLeft.transform.GetChild(0).GetChild(0).transform.position);
            GameObject newCurveLeft = Instantiate(roadParts[2], pos + shift, transform.rotation * curveRot);
            pieces.Add(newCurveLeft);
            currentTargets.Clear();
            currentRotated.Clear();

            foreach (Transform points in newCurveLeft.transform.GetChild(0))
            {
                if (points.tag == "Centre")
                {
                    currentTargets.Add(points);
                    currentRotated = currentTargets.OrderBy(go => go.name).ToList();
                }
            }
            foreach (var item in currentRotated)
            {
                targets.Add(item);
            }
        }

        else
        {
            whatLast = WhatLast.curveRight;
            curveRot = Quaternion.Euler(0, 0, 0);
            shift = new Vector3(16, 0, 64);
            GameObject newCurveRight = Instantiate(roadParts[3], pos + shift, transform.rotation * curveRot);
            pieces.Add(newCurveRight);
            currentTargets.Clear();
            currentRotated.Clear();

            foreach (Transform points in newCurveRight.transform.GetChild(0))
            {
                if (points.tag == "Centre")
                {
                    currentTargets.Add(points);
                    currentRotated = currentTargets.OrderBy(go => go.name).ToList();
                }
            }
            foreach (var item in currentRotated)
            {
                targets.Add(item);
            }
        }


    }

   public void SetFinish()
    {
        GameObject finish = Instantiate(finishPoint, pieces[piecesNumber-1].transform.position, transform.rotation);
    }


}
