using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavManager : MonoBehaviour
{
    RoadManager roadManager;
    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
    // Start is called before the first frame update
    void Start()
    {
        roadManager = GameObject.Find("RoadManager").GetComponent<RoadManager>();
    }

 
    public void SetNavigation()
    {
        surfaces.Clear();
        for (int i = 0; i < roadManager.pieces.Count; i++)
        {
            surfaces.Add(roadManager.pieces[i].gameObject.transform.GetChild(0).GetComponent<NavMeshSurface>());
        }

        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}
