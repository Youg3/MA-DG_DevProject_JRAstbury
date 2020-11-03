using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class CharStats : GenCharStats
{
    //public string charName;
    public int playerLevel = 1;
    public int currentEXP;
    //array of how much experience needed to level up
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    [Header("Character Stats")]
    public Sprite charImage;
    //public int currentHP;
    //public int maxHP = 100;
    //public int currentMP;
    //public int maxMP = 30;
    public int[] mpLevelBonus;
    //public int strength;
    //public int defense;
    //item stats
    //public int weaponPower;
    //public int armourPower;
    public string weaponName;
    public string armourName;

    // Start is called before the first frame update
    void Start()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for(int i = 2; i < expToNextLevel.Length; i++)
        {
            //FloorToInt chops off additional decimal points from the end of the equation
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }

    }

    public void AddExp(int expToAdd)
    {
        currentEXP += expToAdd;

        if (playerLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[playerLevel])
            {
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;

                //determine whether to + Str or + Def based on Odd/Even
                if (playerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }

                //increments max hp and full heal
                maxHp = Mathf.FloorToInt(maxHp * 1.05f);
                currentHp = maxHp;
                //MP level up
                maxMp += mpLevelBonus[playerLevel];
                currentMp = maxMp;
            }
        }

        if (playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }
}
