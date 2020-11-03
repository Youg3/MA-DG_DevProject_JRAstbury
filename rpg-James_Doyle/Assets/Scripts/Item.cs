using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")] public bool isItem;
    public bool isWeapon;
    public bool isArmour;
    [Header("Basic Details")] public string itemName;
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
    
    public void Use(int charToUseOn)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        if (isItem)
        {
            if (effectHP) //use of potion, add amount to change to Char
            {
                selectedChar.currentHp += amountToChange;

                if (selectedChar.currentHp > selectedChar.maxHp)
                {
                    selectedChar.currentHp = selectedChar.maxHp;
                }
            }

            if (effectMP)
            {
                selectedChar.currentMp += amountToChange;

                if (selectedChar.currentMp > selectedChar.maxMp)
                {
                    selectedChar.currentMp = selectedChar.maxMp;
                }
            }

            if (effectStr)
            {
                selectedChar.strength += amountToChange;
            }
            //can put in additional potion types here, str, def, etc...
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
            selectedChar.wpnPwr = weaponStr;
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
            selectedChar.armPwr = armourStr;
        }

        GameManager.instance.RemoveItem(itemName); //remove from inventory

    }

    public void UseItemBattle(int charToUseOn)
    {
        BattleChar selectedBattleChar = BattleManager.instance.activeBattleChar[charToUseOn];

        if (isItem)
        {
            if (effectHP) //use of potion, add amount to change to Char
            {
                selectedBattleChar.currentHp += amountToChange;

                if (selectedBattleChar.currentHp > selectedBattleChar.maxHp)
                {
                    selectedBattleChar.currentHp = selectedBattleChar.maxHp;
                }
            }
            if (effectMP)
            {
                selectedBattleChar.currentMp += amountToChange;

                if (selectedBattleChar.currentMp > selectedBattleChar.maxMp)
                {
                    selectedBattleChar.currentMp = selectedBattleChar.maxMp;
                }
            }
        }

        //need to implement armour and weapon item stuff here

        GameManager.instance.RemoveItem(itemName); //remove from inventory
    }
}

