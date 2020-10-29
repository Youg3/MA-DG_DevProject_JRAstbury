using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModMenu : MonoBehaviour
{
    public static ModMenu instance;

    public GameObject modMenu;
    public GameObject[] modWindows;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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

        //loading screen + main menu
    }
}
