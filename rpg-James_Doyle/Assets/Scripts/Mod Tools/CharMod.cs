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
        //AutoFill(); //fill out stats
    }

    public void AutoFill()
    {
        for (int i = 0; i < charPanels.Length; i++)
        {
            //Debug.Log(i);

            for (int j = 0; j < charPrefabs.Length; j++)
            {
                //Debug.Log(j);

                int selectedChar = charPanels[i].gameObject.GetComponent<SelectChar>().selectChar;

                if (j == selectedChar)
                {
                    charName[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().charName;
                    charHP[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().currentHp.ToString();
                    charMP[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().currentMp.ToString();
                    charStr[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().strength.ToString();
                    charDef[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().defence.ToString();
                    wpnPwr[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().wpnPower.ToString();
                    armourPwr[i].text = charPrefabs[selectedChar].GetComponent<BattleChar>().armPower.ToString();

                    charImage[i].sprite = charPrefabs[selectedChar].GetComponent<SpriteRenderer>().sprite;
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

}
