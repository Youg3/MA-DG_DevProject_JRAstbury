using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance; //instance this so can call it from other scripts

    public GameObject theMenu;
    public GameObject[] windows;

    [Space(5)]

    public GameObject modMenu;
    public GameObject[] modWindows;

    private CharStats[] playerStats;

    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;

    [Header("Stat Menu")]
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

    [Header("Item Menu")]

    public ItemButton[] itemButtons;

    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;

    //for the character selection buttons in item menu
    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    public Text goldText;

    public string mainMenuName;

    public bool battleActive = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            if (!battleActive)
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
                    GameManager.instance.gameMenuOpen = true; //disable the ability to walk
                }

                AudioManager.instance.PlaySFX(5);
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //Debug.Log(modWindows.Length);
            if (modMenu.activeInHierarchy)
            {
                modMenu.SetActive(false);
            }
            else
            {
                modMenu.SetActive(true);
                GameManager.instance.modMenuOpen = true; 
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

        goldText.text = GameManager.instance.currentGold.ToString() + "g"; //display gold on menu screen.
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

        itemCharChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        theMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;
        itemCharChoiceMenu.SetActive(false);
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

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite =
                    GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem) //this updates the item menu buttons and texts
    {
        activeItem = newItem;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        if (activeItem.isArmour || activeItem.isWeapon)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;

            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);

        }

    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharChoice();
    }

    public void SaveGame()
    {
        GameManager.instance.SaveData();
        QuestManager.instance.SaveQuestData();
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(4);
    }

    public void PlayEquipSound()
    {
        AudioManager.instance.PlaySFX(1);
    }

    public void PlayDiscardSound()
    {
        AudioManager.instance.PlaySFX(6);
    }

    public void PlayCharSeletSound()
    {
        AudioManager.instance.PlaySFX(5);
    }

    public void PlayBuyItemSound()
    {
        AudioManager.instance.PlaySFX(8);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(mainMenuName);

        //destroy other active objects in scene
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(gameObject);
    }

    public void ToggleModWindow(int modWindowNumber)
    {
        //activate and deactivate the mod control panels
        for (int i = 0; i < modWindows.Length; i++)
        {
            if (i == modWindowNumber)
            {
                modWindows[i].SetActive(!modWindows[i].activeInHierarchy);
                //Debug.Log(modWindowNumber);
            }
            else
            {
                modWindows[i].SetActive(false);
            }
        }

    }
}
