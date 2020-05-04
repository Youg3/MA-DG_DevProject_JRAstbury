using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;
    [Header("Basic Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    //item stats
    public int amountToChange;
    public bool effectHP, effectMP, effectStr, effectDef;

    [Header("Weapon/Armour Details")]
    //weapon and armour strengths
    public int weaponStr;
    public int armourStr; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUseOn)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        if (isItem)
        {
            if (effectHP) //use of potion, add amount to change to Char
            {
                selectedChar.currentHP += amountToChange;

                if (selectedChar.currentHP > selectedChar.maxHP)
                {
                    selectedChar.currentHP = selectedChar.maxHP;
                }
            }

            if (effectMP)
            {
                selectedChar.currentMP += amountToChange;

                if (selectedChar.currentMP > selectedChar.maxMP)
                {
                    selectedChar.currentMP = selectedChar.maxMP;
                }
            }

            if (effectStr)
            {
                selectedChar.strength += amountToChange;
            }
        }

        if (isWeapon)
        {
            //check to see if the char has a weapon already equipped & add back to inventory
            if (selectedChar.weaponName != "")
            {
                GameManager.instance.AddItem(selectedChar.weaponName);
            }

            //set new item to the char
            selectedChar.weaponName = itemName;
            selectedChar.weaponPower = weaponStr;
        }

        if (isArmour)
        {
            //check to see if the char has armour already equipped & add back to inventory
            if (selectedChar.armourName != "")
            {
                GameManager.instance.AddItem(selectedChar.armourName);
            }

            //set new item to the char
            selectedChar.armourName = itemName;
            selectedChar.armourPower = armourStr;
        }

        GameManager.instance.RemoveItem(itemName); //remove from inventory

    }
}
