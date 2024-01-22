using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    RoadManager roadManager;
    GameManager gameManager;
   
    public float carSpeed;

    private void Start()
    {
        roadManager = GameObject.Find("RoadManager").GetComponent<RoadManager>();
        
    }
    public void ClearOld()
    {
        GameObject forClear = roadManager.pieces[0];
        Destroy(roadManager.pieces[0]);
        roadManager.pieces.Remove(forClear);
    }

   







}
