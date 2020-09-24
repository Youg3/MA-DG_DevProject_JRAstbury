using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ModTools : MonoBehaviour
{
    public static ModTools instance; //accessible to other scripts

    public GameObject pauseBetweenTimer;
    public GameObject eyeballMaxHP; //for later
    //public GameObject battleZone1;
    //public GameObject battleZone2;

    public GameObject[] battleZones;

    public float pauseBetweenBattlesNumber;

    public InputField userInput;


    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //userInput.text = pauseBetweenBattlesNumber.ToString();
            float i = float.Parse(userInput.text);
            Debug.Log(i); //log to console for testing purposes.
            pauseBetweenBattlesNumber = i;
            Debug.Log(pauseBetweenBattlesNumber);

            foreach (var t in battleZones)
            {
                t.GetComponent<BattleStarter>().pauseBetweenBattles = pauseBetweenBattlesNumber;
                Debug.Log("ForEach");
            }

            //BattleStarter.instance.ModCheck();
            //Debug.Log(battleZone1.GetComponent("Pause Between Battles"));
            //Debug.Log(battleZone1.gameObject.GetComponent("Script"));

            //Debug.Log("Battle Starter Number: " + battleZone1.GetComponent("Pause Between Battles"));
        }

        /*if (Input.GetKeyDown(KeyCode.N))
        {
            BattleStarter.instance.pauseBetweenBattles = pauseBetweenBattlesNumber;
            Debug.Log("Set New Battle Starter Number");
        }*/
    }

    



}
