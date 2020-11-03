using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, battleActive, modMenuOpen; //4 separate bools

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems; //ref to Item script

    public int currentGold;
    public bool shopActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems(); //as soon as the game loads, sort items
    }

    // Update is called once per frame
    void Update()
    {
        //player can or cannot move
        if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive || battleActive || modMenuOpen)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
        
        if (Input.GetKeyDown(KeyCode.O)) { SaveData(); }
        if (Input.GetKeyDown(KeyCode.P)) { LoadData(); }
    }

    public Item GetItemDetails(string itemToGrab)
    {
        //search through the reference items array to see if the currently selected item is an actual item
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }
        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    //if it was an actual item that was moved, keep the while loop running
                    if (itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;

            for (int i = 0; i < referenceItems.Length; i++)
            {
                if (referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;

                    i = referenceItems.Length;
                }
            }

            if (itemExists) //adds item to existing pool
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " Does not Exist");
            }

        }
        GameMenu.instance.ShowItems(); //refresh item window
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;

                i = itemsHeld.Length;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Couldn't find: " + itemToRemove);
        }
    }
    public bool HasItem(string searchItem)
    {
        for (int i = 0; i < itemsHeld.Length - 1; i++)
        {
            if (itemsHeld[i] == searchItem)
            {
                return true;
            }
        }
        return false;
    }

    public void SaveData()
    {
        //save scene
        PlayerPrefs.SetString("Current_Scene_", SceneManager.GetActiveScene().name);

        //save where the player is in the world
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        //char details/info
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy) //checks to see which char's are active in the game
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active",1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active",0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Level", playerStats[i].playerLevel); //saves char level
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentHP", playerStats[i].currentHp); //saves char current HP
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_maxHP", playerStats[i].maxHp); //saves char current HP
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentMP", playerStats[i].currentMp); //saves char current MP
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_maxMP", playerStats[i].maxMp); //saves char current MP
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentEXP", playerStats[i].currentEXP); //saves char current EXP
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Strength", playerStats[i].strength); //saves char current STR
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Defense", playerStats[i].defence); //saves char current DEF
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_WpnPwr", playerStats[i].wpnPwr); //saves char current Weapon Power
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_ArmPwr", playerStats[i].armPwr); //saves char current Armour Power
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_Weapon", playerStats[i].weaponName); //saves char current Equipped Weapon
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_Armour", playerStats[i].armourName); //saves char current Equipped Armour
            
        }

        //store Inventory Data
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("Item_In_Inventory_" + i, itemsHeld[i]); //stores item names
            PlayerPrefs.SetInt("Item_Amount_" + i, numberOfItems[i]); //stores amount of items
        }

        //store gold amount
        PlayerPrefs.SetInt("Gold_Amount_", currentGold);

    }

    public void LoadData()
    {
        //load scene
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene_"));
        //loads player position
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false); //inactive player/char
            }
            else
            {
                playerStats[i].gameObject.SetActive(true); //should be active player/char
            }

            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Level"); //get char level
            playerStats[i].currentHp = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentHP"); //saves char current HP
            playerStats[i].maxHp = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_maxHP"); //saves char current HP
            playerStats[i].currentMp = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentMP"); //saves char current MP
            playerStats[i].maxMp = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_maxMP"); //saves char current MP
            playerStats[i].currentEXP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentEXP"); //saves char current EXP
            playerStats[i].strength = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Strength"); //saves char current STR
            playerStats[i].defence = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Defense"); //saves char current DEF
            playerStats[i].wpnPwr = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_WpnPwr"); //saves char current Weapon Power
            playerStats[i].armPwr = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_ArmPwr"); //saves char current Armour Power
            playerStats[i].weaponName = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_Weapon"); //saves char current Equipped Weapon
            playerStats[i].armourName = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_Armour"); //saves char current Equipped Armour
        }

        //inventory
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("Item_In_Inventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("Item_Amount_" + i);
        }

        //load gold amount
        currentGold = PlayerPrefs.GetInt("Gold_Amount_");

    }
}
