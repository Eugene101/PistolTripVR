using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarMenu : MonoBehaviour
{

    float carspeed;
    [SerializeField] float wheelspeed;
    [SerializeField] GameObject carBody;

    [SerializeField] GameObject rightFrontWheel;
    [SerializeField] GameObject leftFrontWheel;
    [SerializeField] GameObject leftBackWheel;
    [SerializeField] GameObject rightBackWheel;
    CarCreator carCreator;
    GameObject carDestroyPosition;

    private void Start()
    {
        carCreator = GameObject.Find("CarController").GetComponent<CarCreator>();
        carDestroyPosition = GameObject.Find("CarDestroypoint");
        carspeed = Random.Range(0.2f, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        //rightFrontWheel.transform.Rotate(Vector3.right, wheelspeed);
        //leftFrontWheel.transform.Rotate(Vector3.right, wheelspeed);
        //leftBackWheel.transform.Rotate(Vector3.right, wheelspeed);
        //rightBackWheel.transform.Rotate(Vector3.right, wheelspeed);
        transform.Translate(Vector3.forward * carspeed);
        float dist = Vector3.Distance(transform.position, carDestroyPosition.transform.position);
        if (dist <= 1f && CarCreator.carIsAlive)
        {
            CarCreator.carIsAlive = false;
            carCreator.CreateCar();
            Destroy(gameObject);
        }
    }
}
