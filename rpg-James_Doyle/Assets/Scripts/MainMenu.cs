using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string newGameScene;

    public GameObject continueButton;
    public GameObject[] panels;

    public string loadGameScene;

    
    void Start()
    {
        //possible count how many times here the menu has opened prior to allowing the player to reset the game to default values?
        if (PlayerPrefs.HasKey("Current_Scene_"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void Continue()
    {
        AudioManager.instance.PlaySFX(4);
        SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame()
    {
        AudioManager.instance.PlaySFX(4);
        SceneManager.LoadScene(newGameScene);
    }

    public void Exit()
    {
        AudioManager.instance.PlaySFX(4);
        Application.Quit();//unity instructions to close game.  cannot test this until build
    }

    public void Panels(int panelNum)
    {
        //controls activating/deactivating panels for Credits and Controls on main menu screen
        panels[panelNum].SetActive(!panels[panelNum].activeInHierarchy);
    }
}
