using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;

    private CharStats[] playerStats;

    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] statusButtons;

    public Text statusName,
        statusHP,
        statusMP,
        statusStr,
        statusDef,
        statusWeaponEq,
        statusWpnPwr,
        statusArmrEq,
        statusArmrPwr,
        statusExp;

    public Image statusImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            //right click to open the menu
            if (theMenu.activeInHierarchy)
            {
                //theMenu.SetActive(false);
                //GameManager.instance.gameMenuOpen = false;

                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);

                //update char stats
                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        //update stats before loading anything else
        UpdateMainStats();

        for (int i = 0; i < windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;
    }

    public void OpenStatus()
    {
        //update any changes before loading stats
        UpdateMainStats();

        StatusChar(0);

        //update displayed info

        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusChar(int selected)
    {
        //loading in all the player stats
        statusName.text = playerStats[selected].charName;
        statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
        statusStr.text = playerStats[selected].strength.ToString();
        statusDef.text = playerStats[selected].defense.ToString();

        if (playerStats[selected].weaponName != "")
        {
            statusWeaponEq.text = playerStats[selected].weaponName;
        }

        statusWpnPwr.text = playerStats[selected].weaponPower.ToString();

        if (playerStats[selected].armourName != "")
        {
            statusArmrEq.text = playerStats[selected].armourName;
        }

        statusArmrPwr.text = playerStats[selected].armourPower.ToString();

        statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] -
                          playerStats[selected].currentEXP).ToString();

        statusImg.sprite = playerStats[selected].charImage;
    }
}
