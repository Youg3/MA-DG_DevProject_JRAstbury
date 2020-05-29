using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItems : MonoBehaviour
{
    public static BattleItems instance;

    public GameObject battleItemMenu;
    //array of buttons for the items to use
    public ItemButton[] itemButtons;
    public ItemButton[] characterButtons;

    [Header("Player Select Buttons")]
    public GameObject useOnWhoText;
    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    public Item selectedItem;
    public Text selectedItemName, selectedItemDescrip, useButtonText;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void OpenMenu()
    {
        battleItemMenu.SetActive(true);
        ShowItems();
    }

    public void CloseMenu()
    {
        itemCharChoiceMenu.SetActive(false);
        useOnWhoText.SetActive(false);
        battleItemMenu.SetActive(false);
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


    public void SelectItem(Item newItem)
    {
        selectedItem = newItem;

        if (selectedItem.isItem)
        {
            useButtonText.text = "Use";
        }
        
        if(selectedItem.isArmour || selectedItem.isWeapon)
        {
            useButtonText.text = "Equip";
        }

        selectedItemName.text = selectedItem.itemName;
        selectedItemDescrip.text = selectedItem.description;

    }

    public void CharChoiceButtons()
    {
        itemCharChoiceMenu.SetActive(true);
        useOnWhoText.SetActive(true);

        for (int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void UseItem(int selectChar)
    {
        selectedItem.UseItemBattle(selectChar);
        BattleManager.instance.NextTurn();
        CloseMenu();

    }
}
