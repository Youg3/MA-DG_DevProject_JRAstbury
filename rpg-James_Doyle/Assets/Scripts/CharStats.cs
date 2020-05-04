using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int playerLevel = 1;
    public int currentEXP;
    //array of how much experience needed to level up
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseEXP = 1000;

    [Header("Character Stats")]
    public Sprite charImage;
    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maxMP = 30;
    public int[] mpLevelBonus;
    public int strength;
    public int defense;
    //item stats
    public int weaponPower;
    public int armourPower;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(1000);
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
                    defense++;
                }

                //increments max hp and full heal
                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;
                //MP level up
                maxMP += mpLevelBonus[playerLevel];
                currentMP = maxMP;
            }
        }

        if (playerLevel >= maxLevel)
        {
            currentEXP = 0;
        }
    }
}
