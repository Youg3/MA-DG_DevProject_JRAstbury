﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMod : MonoBehaviour
{
    public static EnemyMod instance;

    [Header("Panels and Prefabs")]
    public GameObject[] charPrefabs;

    public GameObject[] enemyPanels;

    [Header("Text Values")]
    public Text[] charName;
    public Text[]
        charHP,
        charMP,
        charStr,
        charDef,
        wpnPwr,
        armourPwr;

    public Image[] charImage;

    [Header("Input Values")] 
    public InputField[] newHP;
    public InputField[] newMP, newStr, newDef, newWpnPwr, newArmPwr;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        AutoFill(); //fill out stats
    }

    public void AutoFill()
    {
        for (int i = 0; i < charPrefabs.Length; i++)
        {
            //int selectedEnemy = SelectChar.instance.selectChar;

            int selectedEnemy = enemyPanels[i].gameObject.GetComponent<SelectChar>().selectChar;

            charName[i].text = charPrefabs[selectedEnemy].GetComponent<BattleChar>().charName;
            charHP[i].text = charPrefabs[selectedEnemy].GetComponent<BattleChar>().currentHp.ToString();
            charMP[i].text = charPrefabs[selectedEnemy].GetComponent<BattleChar>().currentMp.ToString();
            charStr[i].text = charPrefabs[selectedEnemy].GetComponent<BattleChar>().strength.ToString();
            charDef[i].text = charPrefabs[selectedEnemy].GetComponent<BattleChar>().defence.ToString();
            wpnPwr[i].text = charPrefabs[selectedEnemy].GetComponent<BattleChar>().wpnPower.ToString();
            armourPwr[i].text = charPrefabs[selectedEnemy].GetComponent<BattleChar>().armPower.ToString();

            charImage[i].sprite = charPrefabs[selectedEnemy].GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void SaveInput()
    {
        for (int i = 0; i < charPrefabs.Length; i++)
        {
            //save button for User inputted values
            if (!string.IsNullOrEmpty(newHP[i].text))
            {
                //handles any potential exceptions from poor user entered values
                int.TryParse(newHP[i].text, out charPrefabs[i].GetComponent<BattleChar>().currentHp);
            }

            if (!string.IsNullOrEmpty(newMP[i].text))
                int.TryParse(newMP[i].text, out charPrefabs[i].GetComponent<BattleChar>().currentMp);

            if (!string.IsNullOrEmpty(newStr[i].text))
                int.TryParse(newStr[i].text, out charPrefabs[i].GetComponent<BattleChar>().strength);

            if (!string.IsNullOrEmpty(newDef[i].text))
                int.TryParse(newDef[i].text, out charPrefabs[i].GetComponent<BattleChar>().defence);

            if (!string.IsNullOrEmpty(newWpnPwr[i].text))
                int.TryParse(newWpnPwr[i].text, out charPrefabs[i].GetComponent<BattleChar>().wpnPower);

            if (!string.IsNullOrEmpty(newArmPwr[i].text))
                int.TryParse(newArmPwr[i].text, out charPrefabs[i].GetComponent<BattleChar>().armPower);

            AutoFill();
        }
    }
}