using System.Collections;
using System.Collections.Generic;
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
        for (int i = 0; i < charPanels.Length; i++)
        {
            //Debug.Log(i);

            for (int j = 0; j < charPrefabs.Length; j++)
            {
                int selectedChar = charPanels[i].gameObject.GetComponent<SelectChar>().selectChar; //get the assigned char number from the gameobject
                if (charPanels[i].tag != "Player")
                {
                    if (j == selectedChar) //compare selected number with prefab
                    {
                        BattleChar enemyPrefabs = charPrefabs[selectedChar].GetComponent<BattleChar>();
                        charName[i].text = enemyPrefabs.charName;
                        charHP[i].text = enemyPrefabs.currentHp.ToString();
                        charMP[i].text = enemyPrefabs.currentMp.ToString();


                        //charName[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().charName;
                        //charHP[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().currentHp.ToString();
                        //charMP[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().currentMp.ToString();
                        charStr[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().strength.ToString();
                        charDef[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().defence.ToString();
                        wpnPwr[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().wpnPower.ToString();
                        armourPwr[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().armPower.ToString();

                        charImage[i].sprite = charPrefabs[selectedChar].GetComponent<SpriteRenderer>().sprite;
                    }

                }
                else if (charPanels[i].tag == "Player" && charPrefabs[j].tag == "Player")
                {
                    //int selectedPlayer = 0;
                    //PlayerSelection(selectedPlayer);
                    //charName[i].text = GameManager.instance.playerStats[selectedPlayer].charName;
                    //Debug.Log(charPrefabs[j] + "/" + charPanels[i]);
                    //Debug.Log("Panel Active: " + charPanels[i].gameObject.activeInHierarchy);

                    //Debug.Log(selectedChar);

                    if (j == selectedChar)
                    {
//                        foreach (var playerChars in GameManager.instance.playerStats)
//                        {
//                            if (charPrefabs[selectedChar].gameObject.GetComponent<BattleChar>().charName == playerChars.charName)
//                            {
//                                Debug.Log(playerChars);
//                            }
//                        }

                        Debug.Log("Selected Char and Panel:" + j + " " + selectedChar);
                        for(int p = 0; p < GameManager.instance.playerStats.Length; p++)
                        {
                            CharStats playerPrefabs = GameManager.instance.playerStats[p];

                            if (charPrefabs[selectedChar].gameObject.GetComponent<BattleChar>().charName == playerPrefabs.charName)
                            {
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

    public void SaveInput()
    {
        for (int i = 0; i < charPanels.Length; i++)
        {
            for (int j = 0; j < charPrefabs.Length; j++)
            {
                int selectedChar = charPanels[i].gameObject.GetComponent<SelectChar>().selectChar;

                if (j == selectedChar)
                {
                    //save button for User inputted values
                    if (!string.IsNullOrEmpty(newHP[i].text))
                    {
                        //handles any potential exceptions from poor user entered values
                        int.TryParse(newHP[i].text, out charPrefabs[selectedChar].GetComponent<BattleChar>().currentHp);
                    }

                    if (!string.IsNullOrEmpty(newMP[i].text))
                        int.TryParse(newMP[i].text, out charPrefabs[selectedChar].GetComponent<BattleChar>().currentMp);

                    if (!string.IsNullOrEmpty(newStr[i].text))
                        int.TryParse(newStr[i].text, out charPrefabs[selectedChar].GetComponent<BattleChar>().strength);

                    if (!string.IsNullOrEmpty(newDef[i].text))
                        int.TryParse(newDef[i].text, out charPrefabs[selectedChar].GetComponent<BattleChar>().defence);

                    if (!string.IsNullOrEmpty(newWpnPwr[i].text))
                        int.TryParse(newWpnPwr[i].text, out charPrefabs[selectedChar].GetComponent<BattleChar>().wpnPower);

                    if (!string.IsNullOrEmpty(newArmPwr[i].text))
                        int.TryParse(newArmPwr[i].text, out charPrefabs[selectedChar].GetComponent<BattleChar>().armPower);

                    AutoFill();
                }
            }
        }
    }

    public void CharActivate()
    {
        //enable/disable selected char
        charActivate = charActivate != true;
        Debug.Log(charActivate);
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

    public void BossEnable(GameObject moddableChar)
    {
        Debug.Log("Enable Char");

        moddableChar.SetActive(charActivate == true);
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
