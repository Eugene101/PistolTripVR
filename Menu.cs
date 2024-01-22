using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static bool isTwoGuns = true;
    public Image pistol1;
    public Image pistol2;

    private void Start()
    {
        pistol2.gameObject.SetActive(false);
        CarMover.iCanGo = false;
        CarMover.isLeftGunGrabbed = false;
        CarMover.isRightGunGrabbed=false;
    }
    public void NewGame()
    {
        SceneManager.UnloadSceneAsync("MainMenuBasic");
        SceneManager.LoadScene("Road");
    }

    public void Pistol1()
    {
        pistol2.gameObject.SetActive(false);
        pistol1.gameObject.SetActive(true);
        isTwoGuns = false;
    }

    public void Pistol2()
    {
        pistol2.gameObject.SetActive(true);
        pistol1.gameObject.SetActive(false);
        isTwoGuns = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
