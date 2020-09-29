using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMod : MonoBehaviour
{
    public static EnemyMod instance;

    public GameObject enemyPrefab;

  
    public Text charName,
        charHP,
        charMP,
        charStr,
        charDef,
        wpnPwr,
        armourPwr;

    public Image charImage;

    public InputField newHP, newMP, newStr, newDef, newWpnPwr, newArmPwr;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        AutoFill(); //fill out stats
    }

    private void AutoFill()
    {
        charName.text = enemyPrefab.GetComponent<BattleChar>().charName;
        charHP.text = enemyPrefab.GetComponent<BattleChar>().currentHp.ToString();
        charMP.text = enemyPrefab.GetComponent<BattleChar>().currentMp.ToString();
        charStr.text = enemyPrefab.GetComponent<BattleChar>().strength.ToString();
        charDef.text = enemyPrefab.GetComponent<BattleChar>().defence.ToString();
        wpnPwr.text = enemyPrefab.GetComponent<BattleChar>().wpnPower.ToString();
        armourPwr.text = enemyPrefab.GetComponent<BattleChar>().armPower.ToString();

        charImage.sprite = enemyPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void SaveInput()
    {
        //save button for User inputted values
        if (!string.IsNullOrEmpty(newHP.text))
        {
            //handles any potential exceptions from poor user entered values
            int.TryParse(newHP.text, out enemyPrefab.GetComponent<BattleChar>().currentHp);
        }

        if (!string.IsNullOrEmpty(newMP.text))
            int.TryParse(newMP.text, out enemyPrefab.GetComponent<BattleChar>().currentMp);

        if (!string.IsNullOrEmpty(newStr.text))
            int.TryParse(newStr.text, out enemyPrefab.GetComponent<BattleChar>().strength);  
        
        if (!string.IsNullOrEmpty(newDef.text))
            int.TryParse(newDef.text, out enemyPrefab.GetComponent<BattleChar>().defence);

        if (!string.IsNullOrEmpty(newWpnPwr.text))
            int.TryParse(newWpnPwr.text, out enemyPrefab.GetComponent<BattleChar>().wpnPower);

        if (!string.IsNullOrEmpty(newArmPwr.text))
            int.TryParse(newArmPwr.text, out enemyPrefab.GetComponent<BattleChar>().armPower);

        AutoFill(); //update displayed values
    }
}
