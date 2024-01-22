using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float carSpeed;

    [SerializeField] GameObject PauseMenu;
    PauseMenu pauseMenu;
    [SerializeField] GameObject endMenu;
    public static bool isLevelEnd;
    [SerializeField] GameObject LaserLeft;
    [SerializeField] GameObject LaserRight;
    // Start is called before the first frame update

    private void Awake()
    {
        pauseMenu = PauseMenu.GetComponent<PauseMenu>();
        endMenu.gameObject.SetActive(false);
        isLevelEnd = false;
    }

    public void LevelEnd()
    {
        isLevelEnd = true;
        endMenu.gameObject.SetActive(true);
        LaserLeft.gameObject.SetActive(true);
        LaserRight.gameObject.SetActive(true);

        GameObject[] pistols = GameObject.FindGameObjectsWithTag("Pistol");
        foreach (GameObject pistol in pistols)
        {
            pistol.SetActive(false);
        }

        GameObject[] enemies1 = GameObject.FindGameObjectsWithTag("EnemyBuilding");
        foreach (var enemy1 in enemies1)
        {
            Destroy(enemy1);
        }

        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("EnemyCar");
        foreach (var enemy2 in enemies2)
        {
            Destroy(enemy2);
        }
    }

}