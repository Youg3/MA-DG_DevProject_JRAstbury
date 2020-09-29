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
        statusMP,
        statusStr,
        statusDef,
        statusWpnPwr,
        statusArmrPwr;

    public Image charImage;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        autoFill(); //fill out stats
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void autoFill()
    {
        charName.text = enemyPrefab.GetComponent<BattleChar>().charName;
        charHP.text = enemyPrefab.GetComponent<BattleChar>().currentHp.ToString();
    }
}
