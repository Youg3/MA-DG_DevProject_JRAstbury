using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string newGameScene;

    public GameObject continueButton;
    public GameObject[] panels;

    public string loadGameScene;

    private bool saveFound = false;


    void Start()
    {
        saveFound = false;
        ContinueButton();
    }

    void Update()
    {
        //cheat key to enable loading of previous save state.
        if (Input.GetKeyDown(KeyCode.RightShift) && PlayerPrefs.HasKey("Current_Scene_"))
        {
            saveFound = true;
            ContinueButton();
        }
    }

    void ContinueButton()
    {
        if (saveFound)
        {
            continueButton.SetActive(true);
        }
        else { continueButton.SetActive(false);}
    }

    public void ButtonFx()
    {
        AudioManager.instance.PlaySFX(4);
    }

    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Exit()
    {
        Application.Quit();//unity instructions to close game.  cannot test this until build
    }

    public void Panels(int panelNum)
    {
        //controls activating/deactivating panels for Credits and Controls on main menu screen
        panels[panelNum].SetActive(!panels[panelNum].activeInHierarchy);
    }
}
