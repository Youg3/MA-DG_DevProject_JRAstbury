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
    public GameObject eyeballEnemy; //for later
    //public GameObject battleZone1;
    //public GameObject battleZone2;

    public GameObject[] battleZones; //array of battlezone gameobjects

    public float pauseBetweenBattlesNumber;

    public InputField userInput;
    public InputField eyeballUserInput;

    public Slider test1;


    void Start()
    {
        instance = this;

        //Auto fill fields with current values
        userInput.text = battleZones[0].GetComponent<BattleStarter>().pauseBetweenBattles.ToString();
        Debug.Log(userInput.text); //check in console log that it is reading correctly.

        eyeballUserInput.text = eyeballEnemy.GetComponent<BattleChar>().maxHp.ToString();
        Debug.Log(eyeballUserInput.text); //check in console log that it is reading correctly.


        test1.onValueChanged.AddListener(delegate { ValueChangeCheck(); }); //callback func for slider, works great!

    }

    private void ValueChangeCheck()
    {
        Debug.Log("Callback func!! " + test1.value); //prints value to console via a callback
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

            //set the new value to each battle zone
            foreach (var t in battleZones)
            {
                t.GetComponent<BattleStarter>().pauseBetweenBattles = pauseBetweenBattlesNumber;
                Debug.Log("ForEach");
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("N pressed");
            Debug.Log("Eyeball Enemy HP is: " + eyeballEnemy.GetComponent<BattleChar>().maxHp);

            eyeballEnemy.GetComponent<BattleChar>().maxHp = int.Parse(eyeballUserInput.text);
            Debug.Log(eyeballUserInput.text);
            Debug.Log("Eyeball Enemy HP is now: " + eyeballEnemy.GetComponent<BattleChar>().maxHp);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(test1.value); //prints value to console
        }
        
        if (Input.GetKeyDown(KeyCode.P)) //this would be how to allow modding of battlezone potential battles, exp and enemies.
        {
            Debug.Log("KeyCode P");

            Debug.Log("Battlezone 0 has: " + battleZones[0].GetComponent<BattleStarter>().potentialBattles.Length);

            Debug.Log("Reward EXP for 0: " + battleZones[0].GetComponent<BattleStarter>().potentialBattles[0].rewardExp); //element 0 reward exp

            foreach (var i in battleZones[0].GetComponent<BattleStarter>().potentialBattles[2].enemies) //prints out the enemies in the element 2 battle
            {
                Debug.Log("Enemy in potential battle: " + i);
            }
        }


    }

    



}
