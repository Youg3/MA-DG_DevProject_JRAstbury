using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public string[] questMarkerNames;
    public bool[] questMarkersComplete;

    public static QuestManager instance;

    void Start()
    {
        instance = this;
        questMarkersComplete = new bool[questMarkerNames.Length]; //set to same length of pre named quests

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(CheckIfComplete("Quest Test"));
            MarkQuestComplete("Quest Test");
            MarkQuestIncomplete("Talk");
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerNames[i] == questToFind)
            {
                return i;
            }
        }

        Debug.LogError("Quest: " + questToFind + "does not exist");
        return 0; //if no quest found, return element 0 which is null
    }

    public bool CheckIfComplete(string questToCheck)
    {
        if (GetQuestNumber(questToCheck) != 0)
        {
            return questMarkersComplete[GetQuestNumber(questToCheck)]; //returns value if public int is not null
        }

        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects()
    {
        QuestObjActivator[] questObjects = FindObjectsOfType<QuestObjActivator>(); //find all objects in scene that have the quest obj activator attached
        if (questObjects.Length > 0)
        {
            for (int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion(); //check against each quest if it's complete
            }
        }
    }
}