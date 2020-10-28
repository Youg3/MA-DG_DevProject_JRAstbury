using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMod : MonoBehaviour
{
    public static WorldMod instance;


    public GameObject[] battleZones;
    public InputField[] bzTimers;
    public GameObject[] pathBlockers;
    public GameObject[] pathExits; //holds the two exit paths separate.

    public Toggle route1;
    public Toggle route2;
    public Toggle route3;

    public Toggle[] routeGroups;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        FillValues();

        route1.onValueChanged.AddListener(delegate { OnToggleChange(0,1); });
        route2.onValueChanged.AddListener(delegate { OnToggleChange(2,3); });
        route3.onValueChanged.AddListener(delegate { OnToggleChange(4,5); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SaveWorldMod();
            FillValues();
        }
    }

    public void FillValues()
    {
        //Fill Battle Zone Timers.
        for (int i = 0; i <= bzTimers.Length -1; i++)
        {
            for (int j = 0; j <= battleZones.Length -1; j++)
            {
                BattleStarter currentZone = battleZones[i].GetComponent<BattleStarter>();
                FillBZTimers(i,currentZone);
            }
        }
        //get toggle values

    }

    public void SaveWorldMod()
    {
        // save Battle Zone Timers...
        for (int i = 0; i <= bzTimers.Length - 1; i++)
        {
            for (int j = 0; j <= battleZones.Length - 1; j++)
            {
                BattleStarter currentZone = battleZones[i].GetComponent<BattleStarter>();
                SaveBZTimers(i, currentZone);
            }
        }
    }

    public void FillBZTimers(int Inputfield, BattleStarter bzNum)
    {
        bzTimers[Inputfield].text = bzNum.pauseBetweenBattles.ToString();
    }

    public void SaveBZTimers(int Inputfield, BattleStarter bzNum)
    {
        float.TryParse(bzTimers[Inputfield].text, out bzNum.pauseBetweenBattles); //save entered value
    }


    public void OverworldObjects()
    {
        //activate/deactivate certain overworld objs.
    }

    public void ExitPath()
    {
        if (route1.isOn)
        {
            pathExits[0].SetActive(!pathExits[0].activeInHierarchy);
        }
    }

    public void OnToggleValueChanged(Toggle name)
    {
        Debug.Log(name);
        Debug.Log(route1);
        //exit logic
        if (name == route1)
        {
            //unlock/lock road
            pathBlockers[0].SetActive(!pathBlockers[0].activeInHierarchy);
            pathBlockers[1].SetActive(!pathBlockers[1].activeInHierarchy);

            //pathExits[0].SetActive(!pathExits[0].activeInHierarchy);

        }
        if (name == route2)
        {
            //unlock/lock road
            pathBlockers[2].SetActive(!pathBlockers[2].activeInHierarchy);
            pathBlockers[3].SetActive(!pathBlockers[3].activeInHierarchy);

        }

        if (name == route3)
        {
            //unlock/lock road
            pathBlockers[4].SetActive(!pathBlockers[4].activeInHierarchy);
            pathBlockers[5].SetActive(!pathBlockers[5].activeInHierarchy);

        }
        RouteThreeLogic();
    }    
    public void OnToggleChange(int blocker1, int blocker2)
    {
        //unlock/lock road
        pathBlockers[blocker1].SetActive(!pathBlockers[blocker1].activeInHierarchy);
        pathBlockers[blocker2].SetActive(!pathBlockers[blocker2].activeInHierarchy);

        RouteThreeLogic(); //call the logic behind deciding which exit should be unlocked.
    }

    public void RouteThreeLogic()
    {
        if (route3.isOn)
        {
            if (!route1.isOn && route2.isOn)
            {
                pathExits[0].SetActive(false);
                pathExits[1].SetActive(true);
            }
            else if (route1.isOn && !route2.isOn)
            {
                pathExits[0].SetActive(true);
                pathExits[1].SetActive(false);
            }
            else if (route1.isOn && route2.isOn)
            {
                pathExits[0].SetActive(false);
                pathExits[1].SetActive(false);
            }
        }
        else if (!route3.isOn)
        {
            if (route1.isOn && route2.isOn)
            {
                pathExits[0].SetActive(false);
                pathExits[1].SetActive(false);
            }
            else if (route1.isOn && !route2.isOn)
            {
                pathExits[0].SetActive(false);
                pathExits[1].SetActive(true);
            }
            else if (!route1.isOn && route2.isOn)
            {
                pathExits[0].SetActive(true);
                pathExits[1].SetActive(false);
            }
        }
    }
}
