using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager : MonoBehaviour
{
    public GameObject PlaneAmbient;
    public GameObject[] cityBig;
    public GameObject[] citySmall;
    public GameObject[] weapon;
    public GameObject car;
    [SerializeField] GameObject[] weaponPlace;
    List<GameObject> strips = new List<GameObject>();
    List<Transform> buildingsPosBig = new List<Transform>();
    List<Transform> buildingsPosSmall = new List<Transform>();
    // Start is called before the first frame update
    public enum Maptype { city, wildWest };
    public Maptype maptype;
    public void AmbientOn()
    {
        if (PlaneAmbient != null)
        {
            PlaneAmbient.SetActive(true);
        }

    }

    public void LoadStripsOff()
    {
        var stripsLR = GameObject.FindGameObjectsWithTag("RoadStrips");
        foreach (var item in stripsLR)
        {
            strips.Add(item);
        }

        foreach (var strip in strips)
        {
            strip.SetActive(false);
        }

    }

    public void LoadStripsOn()
    {
        foreach (var strip in strips)
        {
            strip.SetActive(true);
        }
    }
    public void InstallBuildings()
    {
        var buildingTransformBig = GameObject.FindGameObjectsWithTag("CityDotBig");
        foreach (var item in buildingTransformBig)
        {
            buildingsPosBig.Add(item.transform);
        }

        var buildingTransformSmall = GameObject.FindGameObjectsWithTag("CityDotSmall");
        foreach (var item in buildingTransformSmall)
        {
            buildingsPosSmall.Add(item.transform);
        }

        switch (maptype)
        {
            case Maptype.city:
                for (int i = 0; i < buildingsPosBig.Count; i++)
                {
                    int rand = Random.Range(0, cityBig.Length);
                    GameObject newBuilding = Instantiate(cityBig[rand], buildingsPosBig[i].position, transform.rotation);
                    //newBuilding.transform.localScale = new Vector3(newBuilding.transform.localScale.x*2, newBuilding.transform.localScale.y * 3, newBuilding.transform.localScale.z*4);
                    newBuilding.transform.parent = buildingsPosBig[i];
                    newBuilding.transform.rotation = buildingsPosBig[i].transform.rotation;
                }

                for (int i = 0; i < buildingsPosSmall.Count; i++)
                {
                    int rand = Random.Range(0, citySmall.Length);
                    GameObject newBuilding = Instantiate(citySmall[rand], buildingsPosSmall[i].position, transform.rotation);
                    //newBuilding.transform.localScale = new Vector3(newBuilding.transform.localScale.x*2, newBuilding.transform.localScale.y * 3, newBuilding.transform.localScale.z*4);
                    newBuilding.transform.parent = buildingsPosSmall[i];
                    newBuilding.transform.rotation = buildingsPosSmall[i].transform.rotation;
                }
                GameObject currentWeapon = Instantiate(weapon[0], weaponPlace[0].transform.position, transform.rotation);
                currentWeapon.transform.parent = weaponPlace[0].transform;

                if (Menu.isTwoGuns)
                {
                    GameObject currentWeapon1 = Instantiate(weapon[1], weaponPlace[1].transform.position, transform.rotation);
                    currentWeapon1.transform.parent = weaponPlace[1].transform;
                }                
                break;
            case Maptype.wildWest:
                break;
            default:
                break;
        }



    }
}
