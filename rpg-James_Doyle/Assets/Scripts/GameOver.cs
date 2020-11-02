using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string mainMenuScene;
    public string loadingScene;
    public string firstLevel;


    void Start()
    {
        AudioManager.instance.PlayBGM(4); //play some music
        
        PlayerController.instance.gameObject.SetActive(false);
        //GameMenu.instance.gameObject.SetActive(false); //this line seems to break buttons in my menu. Can only guess it's something to do with item usage as that was an individual challenge
        BattleManager.instance.gameObject.SetActive(false);

        //set the DontDestroy Objs to false to avoid any clashes with other scenes
        ModMenu.instance.modObjs.SetActive(false);

    }

    public void QuitToMain()
    {
        //remove all prefabs from setting so to start a clean run
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadLastSave()
    {
        //remove all prefabs from setting so to start a clean run
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);

        SceneManager.LoadScene(loadingScene);
    }

    // playthrough ends upon completing level, reset and send to main menu
    public void GameLoop()
    {
        Destroy(PlayerController.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }
}
