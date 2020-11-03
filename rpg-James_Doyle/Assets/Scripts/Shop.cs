using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    //refs for game objects
    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public string[] itemsForSale;

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectedItem;
    public Text buyItemName, buyItemDescrip, buyItemValue;
    public Text sellItemName, sellItemDescrip, sellItemValue;

    void Start()
    {
        instance = this;
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu(); //opens this menu by default

        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);

        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyItemButtons[0].Press(); //selects first object in inventory to display text values when opening

        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite =
                    GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }


    }

    public void OpenSellMenu()
    {
        sellItemButtons[0].Press();

        sellMenu.SetActive(true);
        buyMenu.SetActive(false);

        ShowSellItems(); //call private func below to fill the sale menu
    }

    private void ShowSellItems()
    {
        GameManager.instance.SortItems(); //check that items are sorted

        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite =
                    GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectBuyItem(Item buyItem)
    {
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDescrip.text = selectedItem.description;
        buyItemValue.text = "Value: " + selectedItem.value + "g";
    }

    public void SelectSellItem(Item sellItem)
    {
        if (sellItem != null)
        {
            selectedItem = sellItem;
            sellItemName.text = selectedItem.itemName;
            sellItemDescrip.text = selectedItem.description;
            sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * .5f).ToString() + "g";
        }
        else
        {
            selectedItem = GameManager.instance.referenceItems[6]; //in my case the Null_Item in the reference items array. You may call it with a smarter way. I might as well when i get down to it.
            sellItemName.text = GameManager.instance.referenceItems[6].itemName;
            sellItemDescrip.text = GameManager.instance.referenceItems[6].description;
            sellItemValue.text = "Value: " + GameManager.instance.referenceItems[6].value.ToString() + "g";
        }
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            //check if enough cash
            if (GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;
                GameManager.instance.AddItem(selectedItem.itemName);
            }

            goldText.text = GameManager.instance.currentGold.ToString() + "g";
        }
    }

    public void SellItem()
    {
        if (selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * .5f);

            GameManager.instance.RemoveItem(selectedItem.itemName); //remove item from inventory once it's sold
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";

        ShowSellItems(); //refresh the sale menu

        if (!GameManager.instance.HasItem(selectedItem.itemName))
        {
            SelectSellItem(null);
        }
    }
}
