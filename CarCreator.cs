using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCreator : MonoBehaviour
{
    // Start is called before the first frame update
    float instInterval;
    public static bool carIsAlive;
    [SerializeField] GameObject[] cars;
    [SerializeField] float DestroyTime;
    [SerializeField] Transform carPosition;

    void Start()
    {
        //instInterval = DestroyTime + Random.Range(1, 4);
        //Invoke("NewCar", 1f);
        CreateCar();
    }

    public void CreateCar()
    {
        print("Newcar");
        carIsAlive = true;
        int rand = Random.Range(0, cars.Length);
        GameObject newCar = Instantiate(cars[rand], carPosition.position, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        newCar.transform.localScale *= 2.5f;
    }
}
