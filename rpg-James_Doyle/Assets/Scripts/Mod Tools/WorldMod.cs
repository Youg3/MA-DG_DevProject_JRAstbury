using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMod : MonoBehaviour
{
    public static WorldMod instance;


    public GameObject[] battleZones;

    [Header("Text Values")] 
    public Text battleTimer;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        FillValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillValues()
    {
        //fill the open values with current
        battleTimer.text = battleZones[0].GetComponent<BattleStarter>().pauseBetweenBattles.ToString();

    }
}
