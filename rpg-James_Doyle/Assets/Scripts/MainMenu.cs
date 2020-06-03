using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string newGameScene;

    public GameObject continueButton;
    public GameObject creditPanel;

    public string loadGameScene;

    
    void Start()
    {
        if (PlayerPrefs.HasKey("Current_Scene_"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    void Update()
    {
        
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

    public void Credits()
    {
        //open credit panel showing the material used to create this game
        creditPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);
    }
}
