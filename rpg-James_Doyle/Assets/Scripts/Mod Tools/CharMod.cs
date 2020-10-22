using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharMod : MonoBehaviour
{
    public static CharMod instance;

    [Header("Panels and Prefabs")]
    public GameObject[] charPrefabs;
    public GameObject[] charPanels;
    public GameObject[] playerPanels;

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

    private bool charActivate = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        AutoFill(); //fill out stats
    }

    public void AutoFill()
    {
        for (int i = 0; i <= charPanels.Length-1; i++)
        {
            //Debug.Log(i);

            for (int j = 0; j <= charPrefabs.Length-1; j++)
            {
                int selectedChar = charPanels[i].gameObject.GetComponent<SelectChar>().selectChar; //get the assigned char number from the gameobject
                if (charPanels[i].tag != "Player")
                {
                    if (j == selectedChar) //compare selected number with prefab
                    {
                        BattleChar enemyPrefabs = charPrefabs[selectedChar].GetComponent<BattleChar>();
                        //call new update method and fill out panels
                        UpdateChar(i,enemyPrefabs);

                        charImage[i].sprite = charPrefabs[selectedChar].GetComponent<SpriteRenderer>().sprite; //add sprite to GenCharStats Parent Class?
                    }
                }
                else if (charPanels[i].tag == "Player" && charPrefabs[j].tag == "Player")
                {
                    if (j == selectedChar)
                    {
                        Debug.Log("Selected Char and Panel:" + j + " " + selectedChar);
                        for(int p = 0; p <= GameManager.instance.playerStats.Length-1; p++)
                        {
                            CharStats playerPrefabs = GameManager.instance.playerStats[p];

                            if (charPrefabs[j].gameObject.GetComponent<BattleChar>().charName == playerPrefabs.charName)
                            {

                                //UpdateChar(i,playerPrefabs) //send data to another method

                                charName[i].text = playerPrefabs.charName;
                                charHP[i].text = playerPrefabs.currentHP.ToString();
                                charMP[i].text = playerPrefabs.currentMP.ToString();
                                charStr[i].text = playerPrefabs.strength.ToString();
                                charDef[i].text = playerPrefabs.defense.ToString();
                                wpnPwr[i].text = playerPrefabs.weaponPower.ToString();
                                armourPwr[i].text = playerPrefabs.armourPower.ToString();

                                charImage[i].sprite = playerPrefabs.charImage;
                            }
                        }
                    }
                }
            }
        }
    }

    public void UpdateChar(int panelField, GenCharStats character)
    {
        charName[panelField].text = character.charName;
        charHP[panelField].text = character.currentHp.ToString();
        charMP[panelField].text = character.currentMp.ToString();
        charStr[panelField].text = character.strength.ToString();
        charDef[panelField].text = character.defence.ToString();
        wpnPwr[panelField].text = character.wpnPwr.ToString();
        armourPwr[panelField].text = character.armPwr.ToString();
    }

    public void SaveChar(int inputField, GenCharStats character)
    {
        if (!string.IsNullOrEmpty(newHP[inputField].text))
        {
            int.TryParse(newHP[inputField].text, out character.currentHp);
        }
        if (!string.IsNullOrEmpty(newMP[inputField].text))
        {
            int.TryParse(newMP[inputField].text, out character.currentMp);
        }
        if (!string.IsNullOrEmpty(newStr[inputField].text))
        {
            int.TryParse(newStr[inputField].text, out character.strength);
        }
        if(!string.IsNullOrEmpty(newDef[inputField].text))
        {
            int.TryParse(newDef[inputField].text, out character.defence);
        }
        if (!string.IsNullOrEmpty(newWpnPwr[inputField].text))
        {
            int.TryParse(newWpnPwr[inputField].text, out character.wpnPwr);
        }
        if (!string.IsNullOrEmpty(newArmPwr[inputField].text))
        {
            int.TryParse(newArmPwr[inputField].text, out character.armPwr);
        }
    }

    public void SaveInput()
    {
        for (int i = 0; i < charPanels.Length; i++)
        {
            for (int j = 0; j < charPrefabs.Length; j++)
            {
                int selectedChar = charPanels[i].gameObject.GetComponent<SelectChar>().selectChar;
                if (charPanels[i].tag != "Player") 
                {
                    if (j == selectedChar)
                    {
                        BattleChar enemyPrefabs = charPrefabs[selectedChar].GetComponent<BattleChar>();
                        //send to save char method
                        SaveChar(i, enemyPrefabs);

                        AutoFill();
                    }
                }else if (charPanels[i].tag == "Player" && charPrefabs[j].tag == "Player")
                {
                    Debug.Log("Nothing Saved - PC");
                    //TDB
                }
            }
        }
    }

    public void CharImage(int selectedChar)
    {
        Debug.Log("Set Char Image Colour");

        if (charActivate != true)
        {
            Debug.Log("Yo, false");
            //change char image
            charImage[selectedChar].gameObject.GetComponent<Image>().color = new Color32(121, 121, 123, 100);
        }
        else
        {
            //opposite from above
            charImage[selectedChar].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    // *****************************************     CURRENTLY NOT IN USE!!!   *******************************************************************
    public void ActivateChar(GameObject character)
    {
        //sets bool on individual GameObjects to true/false
        character.GetComponent<GenCharStats>().isActive = character.GetComponent<GenCharStats>().isActive != true;
        Debug.Log("SetActive Character");
        Debug.Log(character.GetComponent<GenCharStats>().isActive);
    }
    // ********************************************************************************************************************************************

    public void OverWorldChar(GameObject overworldChar)
    { 
        overworldChar.SetActive(!overworldChar.activeInHierarchy);
        charActivate = overworldChar.activeInHierarchy; //set var to if overworld char is active or not
    }

    public void PlayerChars(int playerChar)
    {
        //sets player characters to active/deactive
        GameManager.instance.playerStats[playerChar].gameObject.SetActive(!GameManager.instance.playerStats[playerChar].gameObject.activeInHierarchy);
    }

    public void PlayerSelection(int playerChar)
    {
        //call the activator for the panel display
        ActivePlayerPanel(playerChar);
        //AutoFill(); //refresh details
    }

    public void ActivePlayerPanel(int panelNumber)
    {
        //activate and deactivate the mod control panels
        for (int i = 0; i < playerPanels.Length; i++)
        {
            if (i == panelNumber)
            {
                playerPanels[i].SetActive(!playerPanels[i].activeInHierarchy);
            }
            else
            {
                playerPanels[i].SetActive(false);
            }
        }
    }

}
