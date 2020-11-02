using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModMenu : MonoBehaviour
{
    public static ModMenu instance;

    public GameObject modMenu;
    public GameObject[] modWindows;


    private float waitToLoad;

    [Space(5)]
    public string mainMenuScene;

    public string firstLevel;

    public GameObject modObjs;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Debug.Log(modWindows.Length);
            if (modMenu.activeInHierarchy)
            {
                modMenu.SetActive(false);
                GameManager.instance.modMenuOpen = false;
            }
            else
            {
                modMenu.SetActive(true);
                GameManager.instance.modMenuOpen = true;
                GameMenu.instance.theMenu.SetActive(false); //prevent the main menu from opening while mod menu is open
            }
        }

/*        if (waitToLoad > 0)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                //main menu
                Destroy(PlayerController.instance.gameObject);
                Destroy(GameMenu.instance.gameObject);
                Destroy(AudioManager.instance.gameObject);
                Destroy(BattleManager.instance.gameObject);

                SceneManager.LoadScene(firstLevel);
                UIFade.instance.FadeFromBlack();
            }
        }*/

    }
    public void ToggleModWindow(int modWindowNumber)
    {
        //activate and deactivate the mod control panels
        for (int i = 0; i < modWindows.Length; i++)
        {
            if (i == modWindowNumber)
            {
                modWindows[i].SetActive(!modWindows[i].activeInHierarchy);
            }
            else
            {
                modWindows[i].SetActive(false);
            }
        }
    }
    //button sound
    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(4);
    }

    //exit and return to main menu
    public void EndButton()
    {
        //call all save methods here?
        CharMod.instance.SaveInput();
        WorldMod.instance.SaveWorldMod();
        //store scene values here??!?! **************************************************************************************************

        DelayCo();

        modMenu.SetActive(false);
        GameManager.instance.modMenuOpen = false;

        //set GameObjects to inactive to avoid potential clashes with other scene/s
        modObjs.gameObject.SetActive(false);

        DelayCo();
        SceneManager.LoadScene(firstLevel);
        GameManager.instance.fadingBetweenAreas = false;

    }

    public IEnumerator DelayCo()
    {
        yield return new WaitForSeconds(1.5f);
    }

    public void DestroyInstances()
    {
        Destroy(PlayerController.instance.gameObject);
        //Destroy(GameMenu.instance.gameObject);
        //Destroy(AudioManager.instance.gameObject);
        //Destroy(BattleManager.instance.gameObject);
    }

    public void OpenMenu()
    {
        modMenu.SetActive(true);
    }
}
