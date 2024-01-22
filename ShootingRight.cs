using System.Collections;
using System.Collections.Generic;
using UltimateXR.Avatar;
using UltimateXR.Core;
using UltimateXR.Devices;
using UnityEngine;

public class ShootingRight : MonoBehaviour
{
    bool wasPressed;
    public GameObject bang;
    public Transform shotPosition;
    public Transform bulletPosition;
    public GameObject bullet;
    public float weaponRange = 100f; // Range of the weapon
    public LayerMask hitLayers; // Layers to consider for hit detection
    //public GameObject car;
    void Start()
    {
        wasPressed = true;
        bang.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (wasPressed && UxrAvatar.LocalAvatarInput.GetButtonsPressDown(UxrHandSide.Right, UxrInputButtons.Trigger))
        {
            wasPressed = false;
            Shoot();
        }
    }

    void Shoot()
    {
        bang.SetActive(true);
        Invoke("EndShoot", 0.2f);
        GameObject newBullet = Instantiate(bullet, bulletPosition.position, transform.rotation);
    }

    void EndShoot()
    {
        bang.SetActive(false);
        wasPressed = true;
    }
}
